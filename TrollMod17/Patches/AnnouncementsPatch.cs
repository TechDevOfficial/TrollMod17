using AmongUs.Data;
using AmongUs.Data.Player;
using Assets.InnerNet;
using BepInEx.Unity.IL2CPP.Utils.Collections;
using Il2CppInterop.Runtime.InteropTypes.Arrays;
using LibCpp2IL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using UnityEngine;
using UnityEngine.Networking;
using HarmonyLib;

namespace TrollMod17.Patches;

[HarmonyPatch(typeof(PlayerAnnouncementData), nameof(PlayerAnnouncementData.SetAnnouncements))]
public static class AnnouncementsPatch
{
    private static Announcement CreateAnnouncement()
    {
        return new Announcement
        {
            Number = 100001,
            Title = "TrollMod",
            SubTitle = "by Manu",
            ShortTitle = "TrollMod",
            Text = "loaded TrollMod v" + TrollModPlugin.ModVersion + "\nmade by Manu\npowered by AUMods_IT",
            Language = 0,
            Date = DateTime.Now.ToString("yyyy-MM-dd"),
            Id = "ModNews"
        };
    }

    static void Prefix(ref Il2CppInterop.Runtime.InteropTypes.Arrays.Il2CppReferenceArray<Announcement> aRange)
    {
        var list = new List<Announcement>(aRange);

        if (!list.Exists(a => a.Number == 100001))
            list.Add(CreateAnnouncement());

        list.Sort((a, b) => string.Compare(b.Date, a.Date, StringComparison.Ordinal));

        var newArr = new Il2CppInterop.Runtime.InteropTypes.Arrays.Il2CppReferenceArray<Announcement>(list.Count);
        for (int i = 0; i < list.Count; i++)
            newArr[i] = list[i];

        aRange = newArr;
    }
}