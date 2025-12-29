using MiraAPI.GameOptions;
using MiraAPI.GameOptions.Attributes;
using MiraAPI.Utilities;
using UnityEngine;

namespace TrollMod17.Options;

public class JesterOptions : AbstractOptionGroup<TrollMod17.Roles.JesterRole>
{
    public override string GroupName => "Jester Options";
    public override Color GroupColor => new Color32(140, 30, 200, 255);
}
