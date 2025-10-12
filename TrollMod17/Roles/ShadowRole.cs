using MiraAPI.Roles;
using MiraAPI.GameOptions;
using TrollMod17.Options;
using UnityEngine;
using Il2CppInterop.Runtime.Attributes;

namespace TrollMod17.Roles;

public class ShadowRole : ImpostorRole, ICustomRole
{
    public string RoleName => "Shadow";
    public string RoleLongDescription => "Strikes from the dark with stealthy precision.";
    public string RoleDescription => RoleLongDescription;
    public Color RoleColor => new Color32(120, 50, 200, 255);
    public ModdedRoleTeams Team => ModdedRoleTeams.Impostor;

    public CustomRoleConfiguration Configuration => new(this)
    {
        CanUseSabotage = true,
        MaxRoleCount = 2,
        UseVanillaKillButton = true,
        CanUseVent = true,
        TasksCountForProgress = false,
    };
}
