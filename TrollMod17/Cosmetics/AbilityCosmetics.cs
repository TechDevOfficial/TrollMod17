using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using Reactor.Utilities;
using Il2CppSystem;

namespace TrollMod17.Cosmetics;

public static class AbilityCosmetics
{
    private static readonly Dictionary<PlayerControl, float> _savedPhantomAlpha = new();
    private static readonly HashSet<PlayerControl> _shielded = new();
    private static readonly HashSet<PlayerControl> _smoked = new();
    private static readonly Dictionary<PlayerControl, Color> _originalColors = new();
    private static readonly HashSet<PlayerControl> _tuned = new();
    private static readonly HashSet<PlayerControl> _glimmered = new();
    private static readonly Dictionary<PlayerControl, float> _glimmerAlpha = new();

    public static void ApplyShadowCloak(PlayerControl p, float alphaFactor)
    {
        if (!p || p.cosmetics == null) return;
        try
        {
            var current = p.cosmetics.GetPhantomRoleAlpha();
            _savedPhantomAlpha[p] = current;
        }
        catch { }
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
        ClearSmokeDarken();
        
        foreach (var pc in PlayerControl.AllPlayerControls)
        {
            if (!pc || pc.cosmetics == null) continue;
            var d = Vector2.Distance(center, pc.GetTruePosition());
            if (d > radius) continue;
            
            if (pc.cosmetics.currentBodySprite != null && pc.cosmetics.currentBodySprite.BodySprite != null)
            {
                var sprite = pc.cosmetics.currentBodySprite.BodySprite;
                _originalColors[pc] = sprite.color;
                
                var darkenedColor = new Color(0.1f, 0.1f, 0.1f, sprite.color.a);
                sprite.color = darkenedColor;
                
                _smoked.Add(pc);
            }
        }
    }

    public static void ClearSmokeDarken()
    {
        foreach (var pc in _smoked)
        {
            if (!pc || pc.cosmetics == null) continue;
            
            if (_originalColors.TryGetValue(pc, out var originalColor) && 
                pc.cosmetics.currentBodySprite != null && 
                pc.cosmetics.currentBodySprite.BodySprite != null)
            {
                pc.cosmetics.currentBodySprite.BodySprite.color = originalColor;
            }
        }
        
        _smoked.Clear();
        _originalColors.Clear();
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

    public static void ApplyTuneGlow(Vector2 center, float radius)
    {
        foreach (var pc in PlayerControl.AllPlayerControls)
        {
            if (!pc || pc.cosmetics == null) continue;
            var d = Vector2.Distance(center, pc.GetTruePosition());
            if (d > radius) continue;
            pc.cosmetics.SetOutline(true, new Nullable<Color>(new Color(1f, 0.9f, 0.2f, 1f)));
            _tuned.Add(pc);
        }
    }

    public static void ClearTuneGlow()
    {
        foreach (var pc in _tuned)
        {
            if (!pc || pc.cosmetics == null) continue;
            pc.cosmetics.SetOutline(false, new Nullable<Color>());
        }
        _tuned.Clear();
    }

    public static void ApplyGlimmerInRadius(Vector2 center, float radius)
    {
        foreach (var pc in PlayerControl.AllPlayerControls)
        {
            if (!pc || pc.cosmetics == null) continue;
            var d = Vector2.Distance(center, pc.GetTruePosition());
            if (d > radius) continue;
            if (!_glimmerAlpha.ContainsKey(pc))
            {
                float a = 1f;
                try { a = pc.cosmetics.GetPhantomRoleAlpha(); } catch { }
                _glimmerAlpha[pc] = a;
            }
            pc.cosmetics.SetPhantomRoleAlpha(0.5f);
            _glimmered.Add(pc);
        }
    }

    public static void ClearGlimmer()
    {
        foreach (var pc in _glimmered)
        {
            if (!pc || pc.cosmetics == null) continue;
            if (_glimmerAlpha.TryGetValue(pc, out var a))
            {
                pc.cosmetics.SetPhantomRoleAlpha(a);
            }
            else
            {
                pc.cosmetics.SetPhantomRoleAlpha(1f);
            }
        }
        _glimmered.Clear();
        _glimmerAlpha.Clear();
    }
}
