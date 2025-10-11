using MiraAPI.Hud;
using MiraAPI.Utilities.Assets;
using UnityEngine;
using TrollMod17.Networking;
using Il2CppSystem;

namespace TrollMod17.Buttons;

public class GuardianShieldButton : CustomActionButton<PlayerControl>
{
    public override string Name => "Shield";
    public override float Cooldown => 25f;
    public override float EffectDuration => 6f;
    public override LoadableAsset<Sprite> Sprite => MiraAssets.RefreshIcon;
    public override int MaxUses => 1;

    public override float Distance => 2.5f;

    public override bool Enabled(RoleBehaviour? role)
    {
        return role is TrollMod17.Roles.GuardianRole;
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
        Target?.cosmetics.SetOutline(active, new Nullable<Color>(new Color(0.2f, 1f, 0.7f)));
    }

    protected override void OnClick()
    {
        if (Target == null) return;
        if (!AmongUsClient.Instance.AmHost) return;
        AbilityRpcs.GuardianStartShield(PlayerControl.LocalPlayer, Target.PlayerId, EffectDuration);
    }
    
    public override void OnEffectEnd()
    {
        ResetTarget();
    }
}
