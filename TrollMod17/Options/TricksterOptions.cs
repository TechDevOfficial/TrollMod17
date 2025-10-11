using MiraAPI.GameOptions;
using MiraAPI.GameOptions.Attributes;
using MiraAPI.Utilities;
using UnityEngine;

namespace TrollMod17.Options;

public class TricksterOptions : AbstractOptionGroup<TrollMod17.Roles.TricksterRole>
{
    public override string GroupName => "Trickster Options";
    public override Color GroupColor => new Color32(255, 160, 30, 255);

    [ModdedNumberOption("Trickster Count", min: 0, max: 2, increment: 1f, formatString: "0")]
    public float TricksterCount { get; set; } = 1f;

    [ModdedNumberOption("Trickster Spawn Chance", min: 0, max: 100, increment: 5f, formatString: "0", suffixType: MiraNumberSuffixes.Percent)]
    public float TricksterSpawnChance { get; set; } = 30f;
}
