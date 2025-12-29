using MiraAPI.Hud;
using MiraAPI.Utilities.Assets;
using TrollMod17.Roles;
using UnityEngine;
using Reactor.Networking.Attributes;
using System.Linq;

namespace TrollMod17.Buttons;

public class MedicReviveButton : CustomActionButton<DeadBody>
{
    public override string Name => "Revive";
    public override float Cooldown => 30f;
    public override float EffectDuration => 0f;
    public override int MaxUses => 2;
    public override LoadableAsset<Sprite> Sprite => null!;

    public override bool IsTargetValid(DeadBody? target)
    {
        return target != null;
    }

    public override DeadBody? GetTarget()
    {
        DeadBody? closestBody = null;
        float closestDistance = float.MaxValue;

        foreach (var body in Object.FindObjectsOfType<DeadBody>())
        {
            if (body == null) continue;
            
            float distance = Vector2.Distance(PlayerControl.LocalPlayer.GetTruePosition(), body.TruePosition);
            if (distance < 2f && distance < closestDistance)
            {
                closestDistance = distance;
                closestBody = body;
            }
        }

        return closestBody;
    }

    protected override void OnClick()
    {
        if (Target == null) return;
        RpcRevivePlayer(Target.ParentId);
    }

    [MethodRpc((uint)RpcCalls.SetName)]
    public void RpcRevivePlayer(byte playerId)
    {
        var playerInfo = GameData.Instance.GetPlayerById(playerId);
        
        if (playerInfo != null && playerInfo.IsDead)
        {
            playerInfo.IsDead = false;
            playerInfo.Disconnected = false;
            
            if (playerInfo.Object != null)
            {
                playerInfo.Object.Revive();
            }

            var body = Object.FindObjectsOfType<DeadBody>().FirstOrDefault(b => b.ParentId == playerId);
            if (body != null)
            {
                Object.Destroy(body.gameObject);
            }
        }
    }

    public override void SetOutline(bool active)
    {
        if (Target != null)
        {
            foreach (var renderer in Target.bodyRenderers)
            {
                renderer.material.SetFloat("_Outline", active ? 1f : 0f);
                if (active)
                {
                    renderer.material.SetColor("_OutlineColor", Color.green);
                }
            }
        }
    }

    public override bool Enabled(RoleBehaviour? role)
    {
        return role is MedicRole;
    }
}
