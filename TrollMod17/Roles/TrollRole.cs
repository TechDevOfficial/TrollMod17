using MiraAPI.GameEnd;
using MiraAPI.Roles;
using MiraAPI.Events;
using MiraAPI.Events.Vanilla.Gameplay;
using System.Collections.Generic;
using TrollMod17.GameOver;
using Reactor;
using UnityEngine;

namespace TrollMod17.Roles;

public class TrollRole : CrewmateRole, ICustomRole
{
    public string RoleName => "Troll";
    public string RoleLongDescription => "You are a Troll! Win when someone kills you.";
    public string RoleDescription => RoleLongDescription;
    public Color RoleColor => new Color32(128, 0, 128, 255);
    public ModdedRoleTeams Team => ModdedRoleTeams.Custom;
    public CustomRoleConfiguration Configuration => new(this)
    {
        CanUseSabotage = false,
        MaxRoleCount = 1,
        UseVanillaKillButton = false,
        CanUseVent = false,
        TasksCountForProgress = false
    };

    public override void OnDeath(DeathReason reason)
    {
        if (reason == DeathReason.Kill && AmongUsClient.Instance.AmHost)
        {
            var trollWinner = new List<NetworkedPlayerInfo> { Player.Data };
            CustomGameOver.Trigger<TrollGameOver>(trollWinner);
        }
    }

    public override bool DidWin(GameOverReason reason)
    {
        return reason == CustomGameOver.GameOverReason<TrollGameOver>();
    }
}