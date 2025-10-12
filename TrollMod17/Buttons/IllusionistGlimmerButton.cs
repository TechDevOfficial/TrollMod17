using MiraAPI.Hud;
using MiraAPI.Utilities.Assets;
using UnityEngine;
using TrollMod17.Networking;
using MiraAPI.GameOptions;
using TrollMod17.Options;

namespace TrollMod17.Buttons;

public class IllusionistGlimmerButton : CustomActionButton
{
    public override string Name => "Glimmer";
    public override float Cooldown => 25f;
    public override float EffectDuration => 6f;
    public override LoadableAsset<Sprite> Sprite => MiraAssets.RoundedBox;
    public override int MaxUses
    {
        get
        {
            var inst = OptionGroupSingleton<IllusionistOptions>.Instance;
            var value = inst != null ? inst.IllusionistMaxUses : global::TrollMod17.Options.MaxUses.One;
            return (int)value;
        }
    }
    public override float InitialCooldown => 10f;
    public override ButtonLocation Location => ButtonLocation.BottomRight;
    private const float Radius = 3.5f;

    public override bool Enabled(RoleBehaviour? role)
    {
        return role is TrollMod17.Roles.IllusionistRole;
    }

    protected override void OnClick()
    {
        var center = PlayerControl.LocalPlayer.GetTruePosition();
        AbilityRpcs.IllusionistStartGlimmer(PlayerControl.LocalPlayer, center, Radius, EffectDuration);
    }

    public override void OnEffectEnd()
    {
        AbilityRpcs.IllusionistEndGlimmer(PlayerControl.LocalPlayer);
    }
}
