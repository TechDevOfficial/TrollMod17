using System.Collections;
using MiraAPI.Hud;
using Reactor.Networking.Attributes;
using Reactor.Networking.Rpc;
using Reactor.Utilities;
using TrollMod17.Cosmetics;
using UnityEngine;

namespace TrollMod17.Networking;

public static class AbilityRpcs
{
    [MethodRpc((uint)TrollRpcCalls.ShadowStartCloak, LocalHandling = RpcLocalHandling.None)]
    public static void ShadowStartCloak(PlayerControl sender, byte playerId, float duration)
    {
        var p = GetPlayer(playerId);
        if (!p) return;
        AbilityCosmetics.ApplyShadowCloak(p, 0.15f);
        Coroutines.Start(RemoveAfter(() => AbilityCosmetics.RemoveShadowCloak(p), duration));
        AbilityCosmetics.SpawnPulseRing(p.GetTruePosition(), new Color(0.4f, 0.1f, 0.7f, 0.9f), 0.35f);
    }

    [MethodRpc((uint)TrollRpcCalls.GuardianStartShield, LocalHandling = RpcLocalHandling.None)]
    public static void GuardianStartShield(PlayerControl sender, byte targetId, float duration)
    {
        var t = GetPlayer(targetId);
        if (!t) return;
        AbilityCosmetics.ApplyShieldTint(t);
        Coroutines.Start(RemoveAfter(() => AbilityCosmetics.RemoveShieldTint(t), duration));
        AbilityCosmetics.SpawnPulseRing(t.GetTruePosition(), new Color(0.2f, 1f, 0.7f, 0.9f), 0.4f);
    }

    [MethodRpc((uint)TrollRpcCalls.TricksterStartSmoke, LocalHandling = RpcLocalHandling.None)]
    public static void TricksterStartSmoke(PlayerControl sender, Vector2 center, float radius, float duration)
    {
        AbilityCosmetics.ApplySmokeDarkenInRadius(center, radius);
        Coroutines.Start(RemoveAfter(AbilityCosmetics.ClearSmokeDarken, duration));
        AbilityCosmetics.SpawnPulseRing(center, new Color(0f, 0f, 0f, 0.85f), 0.35f);
    }

    [MethodRpc((uint)TrollRpcCalls.ShadowStartBlinkBehind, LocalHandling = RpcLocalHandling.None)]
    public static void ShadowStartBlinkBehind(PlayerControl sender, byte attackerId, byte targetId)
    {
        var a = GetPlayer(attackerId);
        var t = GetPlayer(targetId);
        if (!a || !t) return;
        var attackerPos = a.GetTruePosition();
        var targetPos = t.GetTruePosition();
        var dir = (attackerPos - targetPos);
        if (dir.sqrMagnitude < 0.0001f) dir = Vector2.right;
        dir = dir.normalized;
        var behind = targetPos - dir * 0.7f;
        a.transform.position = new Vector3(behind.x, behind.y, a.transform.position.z);
        AbilityCosmetics.SpawnPulseRing(targetPos, new Color(0.6f, 0.1f, 0.9f, 0.9f), 0.25f);
    }

    private static PlayerControl GetPlayer(byte id)
    {
        foreach (var pc in PlayerControl.AllPlayerControls)
        {
            if (pc && pc.PlayerId == id) return pc;
        }
        return null;
    }

    private static IEnumerator RemoveAfter(System.Action action, float seconds)
    {
        float t = 0f;
        while (t < seconds)
        {
            t += Time.deltaTime;
            yield return null;
        }
        action();
    }
}
