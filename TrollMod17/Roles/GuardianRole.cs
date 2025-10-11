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

    [HideFromIl2Cpp]
    public int? GetCount()
    {
        var inst = OptionGroupSingleton<GuardianOptions>.Instance;
        var value = inst != null ? inst.GuardianCount : 1f;
        var count = Mathf.Clamp(Mathf.RoundToInt(value), 0, Configuration.MaxRoleCount);
        return count;
    }

    [HideFromIl2Cpp]
    public int? GetChance()
    {
        var inst = OptionGroupSingleton<GuardianOptions>.Instance;
        var value = inst != null ? inst.GuardianSpawnChance : 35f;
        var chance = Mathf.Clamp(Mathf.RoundToInt(value), 0, 100);
        return chance;
    }
}
