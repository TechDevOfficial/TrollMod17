using MiraAPI.Roles;
using MiraAPI.GameOptions;
using TrollMod17.Options;
using UnityEngine;
using Il2CppInterop.Runtime.Attributes;

namespace TrollMod17.Roles;

public class MedicRole : CrewmateRole, ICustomRole
{
    public string RoleName => "Medic";
    public string RoleLongDescription => "Revives dead crewmates with synchronized abilities.";
    public string RoleDescription => RoleLongDescription;
    public Color RoleColor => new Color32(200, 30, 30, 255);
    public ModdedRoleTeams Team => ModdedRoleTeams.Crewmate;

    public CustomRoleConfiguration Configuration => new(this)
    {
        CanUseSabotage = false,
        MaxRoleCount = 1,
        UseVanillaKillButton = false,
        CanUseVent = false,
        TasksCountForProgress = true,
    };
}
