using MiraAPI.GameOptions;
using MiraAPI.GameOptions.Attributes;
using MiraAPI.Utilities;
using UnityEngine;

namespace TrollMod17.Options;

public class BardOptions : AbstractOptionGroup<TrollMod17.Roles.BardRole>
{
    public override string GroupName => "Bard Options";
    public override Color GroupColor => new Color32(255, 210, 70, 255);

    [ModdedEnumOption("Bard Max Uses", typeof(MaxUses), new[] { "1", "2", "3", "4", "5", "Infinite" })]
    public MaxUses BardMaxUses { get; set; } = MaxUses.One;
}
