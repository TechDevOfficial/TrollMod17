using MiraAPI.Roles;
using UnityEngine;

namespace TrollMod17.Roles;

public class IllusionistRole : ImpostorRole, ICustomRole
{
    public string RoleName => "Illusionist";
    public string RoleLongDescription => "Bends light to haze nearby vision.";
    public string RoleDescription => RoleLongDescription;
    public Color RoleColor => new Color32(140, 200, 255, 255);
    public ModdedRoleTeams Team => ModdedRoleTeams.Impostor;

    public CustomRoleConfiguration Configuration => new(this)
    {
        CanUseSabotage = false,
        MaxRoleCount = 2,
        UseVanillaKillButton = false,
        CanUseVent = true,
        TasksCountForProgress = false,
    };
}
