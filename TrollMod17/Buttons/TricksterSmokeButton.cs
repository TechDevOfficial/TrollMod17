using MiraAPI.Hud;
using MiraAPI.Utilities.Assets;
using UnityEngine;
using TrollMod17.Networking;
using TrollMod17.Settings;
using MiraAPI.GameOptions;
using TrollMod17.Options;

namespace TrollMod17.Buttons;

public class TricksterSmokeButton : CustomActionButton
{
    public override string Name => "Smoke";
    public override float Cooldown => 25f;
    public override float EffectDuration => 6f;
    public override LoadableAsset<Sprite> Sprite => MiraAssets.RefreshIcon;
    public override int MaxUses
    {
        get
        {
            var inst = OptionGroupSingleton<TricksterOptions>.Instance;
            var value = inst != null ? inst.TricksterMaxUses : global::TrollMod17.Options.MaxUses.One;
            return (int)value; // Infinite maps to 0
        }
    }

    public override float InitialCooldown => 10f;
    public override ButtonLocation Location => TMLocalSettings.ButtonsPosition == ButtonPos.Left ? ButtonLocation.BottomLeft : ButtonLocation.BottomRight;
    private const float SmokeRadius = 3.5f;

    public override bool Enabled(RoleBehaviour? role)
    {
        return role is TrollMod17.Roles.TricksterRole;
    }

    protected override void OnClick()
    {
        var center = PlayerControl.LocalPlayer.GetTruePosition();
        AbilityRpcs.TricksterStartSmoke(PlayerControl.LocalPlayer, center, SmokeRadius, EffectDuration);
    }

    public override void OnEffectEnd()
    {
        AbilityRpcs.TricksterEndSmoke(PlayerControl.LocalPlayer);
    }
}
