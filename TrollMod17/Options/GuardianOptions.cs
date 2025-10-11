using MiraAPI.GameOptions;
using MiraAPI.GameOptions.Attributes;
using MiraAPI.Utilities;
using UnityEngine;

namespace TrollMod17.Options;

public class GuardianOptions : AbstractOptionGroup<TrollMod17.Roles.GuardianRole>
{
    public override string GroupName => "Guardian Options";
    public override Color GroupColor => new Color32(30, 200, 140, 255);

    [ModdedNumberOption("Guardian Count", min: 0, max: 2, increment: 1f, formatString: "0")]
    public float GuardianCount { get; set; } = 1f;

    [ModdedNumberOption("Guardian Spawn Chance", min: 0, max: 100, increment: 5f, formatString: "0", suffixType: MiraNumberSuffixes.Percent)]
    public float GuardianSpawnChance { get; set; } = 35f;
}
