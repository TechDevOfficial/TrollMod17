using MiraAPI.Roles;
using UnityEngine;

namespace TrollMod17.Roles;

public class BardRole : CrewmateRole, ICustomRole
{
    public string RoleName => "Bard";
    public string RoleLongDescription => "Plays a tune that blesses nearby crewmates.";
    public string RoleDescription => RoleLongDescription;
    public Color RoleColor => new Color32(255, 210, 70, 255);
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
