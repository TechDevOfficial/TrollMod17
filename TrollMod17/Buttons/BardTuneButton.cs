using MiraAPI.Hud;
using MiraAPI.Utilities.Assets;
using UnityEngine;
using TrollMod17.Networking;
using MiraAPI.GameOptions;
using TrollMod17.Options;

namespace TrollMod17.Buttons;

public class BardTuneButton : CustomActionButton
{
    public override string Name => "Tune";
    public override float Cooldown => 25f;
    public override float EffectDuration => 6f;
    public override LoadableAsset<Sprite> Sprite => MiraAssets.RoundedBox;
    public override int MaxUses
    {
        get
        {
            var inst = OptionGroupSingleton<BardOptions>.Instance;
            var value = inst != null ? inst.BardMaxUses : global::TrollMod17.Options.MaxUses.One;
            return (int)value;
        }
    }
    public override float InitialCooldown => 10f;
    public override ButtonLocation Location => ButtonLocation.BottomLeft;
    private const float Radius = 3.5f;

    public override bool Enabled(RoleBehaviour? role)
    {
        return role is TrollMod17.Roles.BardRole;
    }

    protected override void OnClick()
    {
        var center = PlayerControl.LocalPlayer.GetTruePosition();
        AbilityRpcs.BardStartTune(PlayerControl.LocalPlayer, center, Radius, EffectDuration);
    }

    public override void OnEffectEnd()
    {
        AbilityRpcs.BardEndTune(PlayerControl.LocalPlayer);
    }
}
