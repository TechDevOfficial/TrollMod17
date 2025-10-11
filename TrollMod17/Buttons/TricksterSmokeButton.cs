using MiraAPI.Hud;
using MiraAPI.Utilities.Assets;
using UnityEngine;
using TrollMod17.Networking;

namespace TrollMod17.Buttons;

public class TricksterSmokeButton : CustomActionButton
{
    public override string Name => "Smoke";
    public override float Cooldown => 25f;
    public override float EffectDuration => 6f;
    public override LoadableAsset<Sprite> Sprite => MiraAssets.RefreshIcon;
    public override int MaxUses => 1;

    public override float InitialCooldown => 10f;
    private const float SmokeRadius = 3.5f;

    public override bool Enabled(RoleBehaviour? role)
    {
        return role is TrollMod17.Roles.TricksterRole;
    }

    protected override void OnClick()
    {
        if (!AmongUsClient.Instance.AmHost) return;
        var center = PlayerControl.LocalPlayer.GetTruePosition();
        AbilityRpcs.TricksterStartSmoke(PlayerControl.LocalPlayer, center, SmokeRadius, EffectDuration);
    }
}
