using MiraAPI.Roles;
using MiraAPI.GameOptions;
using TrollMod17.Options;
using UnityEngine;
using Il2CppInterop.Runtime.Attributes;

namespace TrollMod17.Roles;

public class GuardianRole : CrewmateRole, ICustomRole
{
    public string RoleName => "Guardian";
    public string RoleLongDescription => "Protects allies with timely shields.";
    public string RoleDescription => RoleLongDescription;
    public Color RoleColor => new Color32(30, 200, 140, 255);
    public ModdedRoleTeams Team => ModdedRoleTeams.Crewmate;

    public CustomRoleConfiguration Configuration => new(this)
    {
        CanUseSabotage = false,
        MaxRoleCount = 2,
        UseVanillaKillButton = false,
        CanUseVent = false,
        TasksCountForProgress = true,
    };
}
