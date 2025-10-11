using BepInEx;
using BepInEx.Configuration;
using BepInEx.Unity.IL2CPP;
using HarmonyLib;
using MiraAPI;
using MiraAPI.PluginLoading;
using MiraAPI.Utilities;
using Reactor;
using Reactor.Networking;
using Reactor.Networking.Attributes;
using Reactor.Patches;
using Reactor.Utilities;
using UnityEngine;

namespace TrollMod17;

[BepInAutoPlugin("manu.trollmod", "TrollMod")]
[BepInProcess("Among Us.exe")]
[BepInDependency(ReactorPlugin.Id)]
[BepInDependency(MiraApiPlugin.Id)]
[ReactorModFlags(ModFlags.RequireOnAllClients)]
public partial class TrollModPlugin : BasePlugin, IMiraPlugin
{
    public Harmony Harmony { get; } = new(Id);
    public string OptionsTitleText => "Troll\nMod";
    public ConfigFile GetConfigFile() => Config;
    public static string ModVersion = "1.4.9";

    public override void Load()
    {
        ReactorCredits.Register("made by Manu - powered by AUMods_IT", "", false, location => location == ReactorCredits.Location.PingTracker);
        ReactorCredits.Register("loaded TrollMod v" + ModVersion + " by Manu", "", false, location => location == ReactorCredits.Location.MainMenu);
        Harmony.PatchAll();
    }
}