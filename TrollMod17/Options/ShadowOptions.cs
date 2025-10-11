using MiraAPI.GameOptions;
using MiraAPI.GameOptions.Attributes;
using MiraAPI.Utilities;
using UnityEngine;

namespace TrollMod17.Options;

public class ShadowOptions : AbstractOptionGroup<TrollMod17.Roles.ShadowRole>
{
    public override string GroupName => "Shadow Options";
    public override Color GroupColor => new Color32(120, 50, 200, 255);

    [ModdedNumberOption("Shadow Count", min: 0, max: 2, increment: 1f, formatString: "0")]
    public float ShadowCount { get; set; } = 1f;

    [ModdedNumberOption("Shadow Spawn Chance", min: 0, max: 100, increment: 5f, formatString: "0", suffixType: MiraNumberSuffixes.Percent)]
    public float ShadowSpawnChance { get; set; } = 40f;
}
