using MiraAPI.GameOptions;
using MiraAPI.GameOptions.Attributes;
using MiraAPI.Utilities;
using UnityEngine;

namespace TrollMod17.Options;

public class MedicOptions : AbstractOptionGroup<TrollMod17.Roles.MedicRole>
{
    public override string GroupName => "Medic Options";
    public override Color GroupColor => new Color32(200, 30, 30, 255);

    [ModdedEnumOption("Medic Max Uses", typeof(MaxUses), new[] { "1", "2", "3", "4", "5", "Infinite" })]
    public MaxUses MedicMaxUses { get; set; } = MaxUses.Two;
}
