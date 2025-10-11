using MiraAPI.Hud;
using MiraAPI.Utilities.Assets;
using UnityEngine;
using TrollMod17.Networking;
using Il2CppSystem;
using TrollMod17.Settings;

namespace TrollMod17.Buttons;

public class ShadowCloakButton : CustomActionButton<PlayerControl>
{
    public override string Name => "Cloak";
    public override float Cooldown => 25f;
    public override float EffectDuration => 6f;
    public override LoadableAsset<Sprite> Sprite => MiraAssets.RefreshIcon;
    public override int MaxUses => 1;
    public override float Distance => 2.6f;
    public override ButtonLocation Location => TMLocalSettings.ButtonsPosition == ButtonPos.Left ? ButtonLocation.BottomLeft : ButtonLocation.BottomRight;

    public override bool Enabled(RoleBehaviour? role)
    {
        return role is TrollMod17.Roles.ShadowRole;
    }

    public override PlayerControl? GetTarget()
    {
        var me = PlayerControl.LocalPlayer;
        if (me == null) return null;
        PlayerControl? best = null;
        var bestDist = float.MaxValue;
        foreach (var pc in PlayerControl.AllPlayerControls)
        {
            if (!pc || pc == me) continue;
            if (pc.Data == null || pc.Data.IsDead) continue;
            var d = Vector2.Distance(me.GetTruePosition(), pc.GetTruePosition());
            if (d <= Distance && d < bestDist)
            {
                best = pc;
                bestDist = d;
            }
        }
        return best;
    }

    public override bool IsTargetValid(PlayerControl? target)
    {
        return target && target.Data != null && !target.Data.IsDead;
    }

    public override void SetOutline(bool active)
    {
        Target?.cosmetics.SetOutline(active, new Nullable<Color>(new Color(0.6f, 0.2f, 0.9f)));
    }

    protected override void OnClick()
    {
        if (!AmongUsClient.Instance.AmHost) return;
        var me = PlayerControl.LocalPlayer;
        if (Target)
        {
            AbilityRpcs.ShadowStartBlinkBehind(me, me.PlayerId, Target.PlayerId);
        }
        AbilityRpcs.ShadowStartCloak(me, me.PlayerId, EffectDuration);
    }

    public override void OnEffectEnd()
    {
        if (!AmongUsClient.Instance.AmHost) return;
        var me = PlayerControl.LocalPlayer;
        AbilityRpcs.ShadowEndCloak(me, me.PlayerId);
    }
}
