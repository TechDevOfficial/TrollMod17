using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using Reactor.Utilities;
using Il2CppSystem;

namespace TrollMod17.Cosmetics;

public static class AbilityCosmetics
{
    // Among Us native cosmetics-backed state
    private static readonly Dictionary<PlayerControl, float> _savedPhantomAlpha = new();
    private static readonly HashSet<PlayerControl> _shielded = new();
    private static readonly HashSet<PlayerControl> _smoked = new();

    public static void ApplyShadowCloak(PlayerControl p, float alphaFactor)
    {
        if (!p || p.cosmetics == null) return;
        // Save current phantom alpha, then set reduced alpha
        try
        {
            var current = p.cosmetics.GetPhantomRoleAlpha();
            _savedPhantomAlpha[p] = current;
        }
        catch { /* ignore */ }
        p.cosmetics.SetPhantomRoleAlpha(Mathf.Clamp01(alphaFactor));
    }

    public static void RemoveShadowCloak(PlayerControl p)
    {
        if (!p || p.cosmetics == null) return;
        if (_savedPhantomAlpha.TryGetValue(p, out var old))
        {
            p.cosmetics.SetPhantomRoleAlpha(old);
            _savedPhantomAlpha.Remove(p);
        }
        else
        {
            // fallback reset
            p.cosmetics.SetPhantomRoleAlpha(1f);
        }
    }

    public static void ApplyShieldTint(PlayerControl p)
    {
        if (!p || p.cosmetics == null) return;
        p.cosmetics.SetOutline(true, new Nullable<Color>(new Color(0.2f, 1f, 0.7f, 1f)));
        _shielded.Add(p);
    }

    public static void RemoveShieldTint(PlayerControl p)
    {
        if (!p || p.cosmetics == null) return;
        if (_shielded.Contains(p))
        {
            p.cosmetics.SetOutline(false, new Nullable<Color>());
            _shielded.Remove(p);
        }
    }

    public static void ApplySmokeDarkenInRadius(Vector2 center, float radius)
    {
        foreach (var pc in PlayerControl.AllPlayerControls)
        {
            if (!pc || pc.cosmetics == null) continue;
            var d = Vector2.Distance(center, pc.GetTruePosition());
            if (d > radius) continue;
            pc.cosmetics.FadeBlackCosmetics(0.6f);
            _smoked.Add(pc);
        }
    }

    public static void ClearSmokeDarken()
    {
        foreach (var p in _smoked)
        {
            if (!p || p.cosmetics == null) continue;
            p.cosmetics.FadeBlackCosmetics(0f);
        }
        _smoked.Clear();
    }

    public static void SpawnPulseRing(Vector2 center, Color color, float duration)
    {
        Coroutines.Start(SpawnPulseRingCo(center, color, duration));
    }

    private static IEnumerator SpawnPulseRingCo(Vector2 center, Color color, float duration)
    {
        var go = new GameObject("AbilityPulse");
        var sr = go.AddComponent<SpriteRenderer>();
        sr.sprite = MiraAPI.Utilities.Assets.MiraAssets.RoundedBox.LoadAsset();
        sr.color = color;
        go.transform.position = new Vector3(center.x, center.y, -5f);
        go.transform.localScale = Vector3.zero;

        float t = 0f;
        while (t < duration)
        {
            t += Time.deltaTime;
            var k = Mathf.Clamp01(t / duration);
            var scale = Mathf.Lerp(0.2f, 1.6f, k);
            go.transform.localScale = new Vector3(scale, scale, 1f);
            var c = sr.color;
            c.a = Mathf.Lerp(color.a, 0f, k);
            sr.color = c;
            yield return null;
        }

        if (go) UnityEngine.Object.Destroy(go);
    }
}
