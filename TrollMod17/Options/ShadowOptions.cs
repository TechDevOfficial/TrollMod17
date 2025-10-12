using MiraAPI.GameOptions;
using MiraAPI.GameOptions.Attributes;
using MiraAPI.Utilities;
using UnityEngine;

namespace TrollMod17.Options;

public class ShadowOptions : AbstractOptionGroup<TrollMod17.Roles.ShadowRole>
{
    public override string GroupName => "Shadow Options";
    public override Color GroupColor => new Color32(120, 50, 200, 255);

    [ModdedEnumOption("Shadow Max Uses", typeof(MaxUses), new[] { "1", "2", "3", "4", "5", "Infinite" })]
    public MaxUses ShadowMaxUses { get; set; } = MaxUses.One;
}
