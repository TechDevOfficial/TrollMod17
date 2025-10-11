using System.Collections;
using MiraAPI.Hud;
using MiraAPI.Utilities.Assets;
using UnityEngine;
using BepInEx.Unity.IL2CPP.Utils.Collections;
using MiraAPI.GameOptions;
using TrollMod17.Options;
using TrollMod17.Utilities;
using Reactor.Utilities;
using MiraAPI.Keybinds;
using Rewired;

namespace TrollMod17.Buttons;

public class WardenLockButton : CustomActionButton<PlainDoor>
{
    public override string Name => "Lock";
    public override float Cooldown => 25f;
    public override LoadableAsset<Sprite> Sprite => TMAssets.LockButton;
    public override int MaxUses
        => Mathf.Clamp(
            Mathf.RoundToInt(OptionGroupSingleton<WardenOptions>.Instance.WardenMaxUses),
            0,
            5);

    public override float Distance => 2.2f;

    public override bool Enabled(RoleBehaviour? role)
    {
        return role is TrollMod17.Roles.WardenRole;
    }

    public override PlainDoor? GetTarget()
    {
        var allDoors = Object.FindObjectsOfType<PlainDoor>();
        var pos = PlayerControl.LocalPlayer.GetTruePosition();
        PlainDoor? best = null;
        var bestDist = float.MaxValue;
        foreach (var d in allDoors)
        {
            var dpos = (Vector2)d.transform.position;
            var dist = Vector2.Distance(pos, dpos);
            if (dist <= Distance && dist < bestDist)
            {
                best = d;
                bestDist = dist;
            }
        }
        return best;
    }

    public override bool IsTargetValid(PlainDoor? target)
    {
        return target != null;
    }

    public override void SetOutline(bool active)
    {
    }

    protected override void OnClick()
    {
        if (Target == null)
            return;

        if (!AmongUsClient.Instance.AmHost)
        {
            return;
        }

        TryCloseDoor(Target);
        Coroutines.Start(ReopenAfterDelay(Target, 5f));
    }

    private static void TryCloseDoor(PlainDoor door)
    {
        door.SetDoorway(false);
        door.UpdateShadow();
    }

    private static IEnumerator ReopenAfterDelay(PlainDoor door, float seconds)
    {
        float t = 0f;
        while (t < seconds)
        {
            t += Time.deltaTime;
            yield return null;
        }

        if (door)
        {
            door.SetDoorway(true);
            door.UpdateShadow();
        }
    }
}
