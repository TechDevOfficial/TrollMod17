using MiraAPI.GameOptions;
using MiraAPI.GameOptions.Attributes;
using MiraAPI.Utilities;
using UnityEngine;

namespace TrollMod17.Options;

public class IllusionistOptions : AbstractOptionGroup<TrollMod17.Roles.IllusionistRole>
{
    public override string GroupName => "Illusionist Options";
    public override Color GroupColor => new Color32(140, 200, 255, 255);

    [ModdedEnumOption("Illusionist Max Uses", typeof(MaxUses), new[] { "1", "2", "3", "4", "5", "Infinite" })]
    public MaxUses IllusionistMaxUses { get; set; } = MaxUses.One;
}
