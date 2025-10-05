using MiraAPI.GameOptions;
using MiraAPI.GameOptions.Attributes;
using MiraAPI.Utilities;
using UnityEngine;

namespace TrollMod17.Options;

public class WardenOptions : AbstractOptionGroup<TrollMod17.Roles.WardenRole>
{
    public override string GroupName => "Warden Options";
    public override Color GroupColor => new Color32(30, 144, 255, 255);

    [ModdedNumberOption("Warden Count", min: 0, max: 3, increment: 1f, formatString: "0")]
    public float WardenCount { get; set; } = 1f;

    [ModdedNumberOption("Warden Spawn Chance", min: 0, max: 100, increment: 5f, formatString: "0", suffixType: MiraNumberSuffixes.Percent)]
    public float WardenSpawnChance { get; set; } = 50f;

    [ModdedNumberOption("Warden Max Uses", min: 0, max: 5, increment: 1f, formatString: "0")]
    public float WardenMaxUses { get; set; } = 1f;
}
