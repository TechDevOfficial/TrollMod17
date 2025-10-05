using MiraAPI.Roles;
using MiraAPI.GameOptions;
using TrollMod17.Options;
using UnityEngine;

namespace TrollMod17.Roles;

public class WardenRole : CrewmateRole, ICustomRole
{
    public string RoleName => "Warden";
    public string RoleLongDescription => "Temporarily lock nearby doors to protect the area.";
    public string RoleDescription => RoleLongDescription;
    public Color RoleColor => new Color32(30, 144, 255, 255);
    public ModdedRoleTeams Team => ModdedRoleTeams.Crewmate;

    public CustomRoleConfiguration Configuration => new(this)
    {
        CanUseSabotage = false,
        MaxRoleCount = 3,
        UseVanillaKillButton = false,
        CanUseVent = false,
        TasksCountForProgress = true,
    };

    public int? GetCount()
    {
        var inst = OptionGroupSingleton<WardenOptions>.Instance;
        var value = inst != null ? inst.WardenCount : 1f;
        var count = Mathf.Clamp(Mathf.RoundToInt(value), 0, Configuration.MaxRoleCount);
        return count;
    }

    public int? GetChance()
    {
        var inst = OptionGroupSingleton<WardenOptions>.Instance;
        var value = inst != null ? inst.WardenSpawnChance : 50f;
        var chance = Mathf.Clamp(Mathf.RoundToInt(value), 0, 100);
        return chance;
    }
}
