using MiraAPI.Roles;
using MiraAPI.GameOptions;
using TrollMod17.Options;
using UnityEngine;
using Il2CppInterop.Runtime.Attributes;

namespace TrollMod17.Roles;

public class JesterRole : CrewmateRole, ICustomRole
{
    public string RoleName => "Jester";
    public string RoleLongDescription => "Wins by getting voted out during meetings.";
    public string RoleDescription => RoleLongDescription;
    public Color RoleColor => new Color32(140, 30, 200, 255);
    public ModdedRoleTeams Team => ModdedRoleTeams.Custom;

    public CustomRoleConfiguration Configuration => new(this)
    {
        CanUseSabotage = false,
        MaxRoleCount = 1,
        UseVanillaKillButton = false,
        CanUseVent = false,
        TasksCountForProgress = false,
        CanGetKilled = true,
    };
}
