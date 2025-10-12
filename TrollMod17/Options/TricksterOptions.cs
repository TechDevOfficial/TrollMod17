using MiraAPI.GameOptions;
using MiraAPI.GameOptions.Attributes;
using MiraAPI.Utilities;
using UnityEngine;

namespace TrollMod17.Options;

public class TricksterOptions : AbstractOptionGroup<TrollMod17.Roles.TricksterRole>
{
    public override string GroupName => "Trickster Options";
    public override Color GroupColor => new Color32(255, 160, 30, 255);

    [ModdedEnumOption("Trickster Max Uses", typeof(MaxUses), new[] { "1", "2", "3", "4", "5", "Infinite" })]
    public MaxUses TricksterMaxUses { get; set; } = MaxUses.One;
}
