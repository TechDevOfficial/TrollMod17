using MiraAPI.Roles;
using MiraAPI.GameOptions;
using TrollMod17.Options;
using UnityEngine;
using Il2CppInterop.Runtime.Attributes;

namespace TrollMod17.Roles;

public class TricksterRole : ImpostorRole, ICustomRole
{
    public string RoleName => "Trickster";
    public string RoleLongDescription => "Sows chaos and manipulates for personal gain.";
    public string RoleDescription => RoleLongDescription;
    public Color RoleColor => new Color32(255, 160, 30, 255);
    public ModdedRoleTeams Team => ModdedRoleTeams.Custom;

    public CustomRoleConfiguration Configuration => new(this)
    {
        CanUseSabotage = false,
        UseVanillaKillButton = false,
        CanUseVent = false,
        TasksCountForProgress = false,
    };
}
