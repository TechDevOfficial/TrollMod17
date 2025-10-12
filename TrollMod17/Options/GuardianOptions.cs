using MiraAPI.GameOptions;
using MiraAPI.GameOptions.Attributes;
using MiraAPI.Utilities;
using UnityEngine;

namespace TrollMod17.Options;

public class GuardianOptions : AbstractOptionGroup<TrollMod17.Roles.GuardianRole>
{
    public override string GroupName => "Guardian Options";
    public override Color GroupColor => new Color32(30, 200, 140, 255);

    [ModdedEnumOption("Guardian Max Uses", typeof(MaxUses), new[] { "1", "2", "3", "4", "5", "Infinite" })]
    public MaxUses GuardianMaxUses { get; set; } = MaxUses.One;
}
