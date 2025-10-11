# TrollMod17 Abilities and Effects

## Shadow
- Name: Cloak + Blink Behind
- Button: `Buttons/ShadowCloakButton.cs` (CustomActionButton<PlayerControl>)
- How it works:
  - Targets nearest alive player within ~2.6 units (like Warden targeting style).
  - On click (host):
    - RPC `AbilityRpcs.ShadowStartBlinkBehind(attackerId, targetId)` teleports Shadow just behind target and spawns a purple pulse ring.
    - RPC `AbilityRpcs.ShadowStartCloak(playerId, duration)` applies a cloak (reduced opacity) to the Shadow for the duration and spawns a pulse ring.
- Visuals:
  - Pulse rings spawned via `AbilityCosmetics.SpawnPulseRing`.
  - Cloak currently lowers alpha via SpriteRenderers (can be switched to AU cosmetics layer).
- Defaults:
  - Cooldown: 25s. Cloak duration: 6s. Uses: 1.

## Guardian
- Name: Shield Ally
- Button: `Buttons/GuardianShieldButton.cs` (CustomActionButton<PlayerControl>)
- How it works:
  - Targets nearest alive ally within ~2.5 units.
  - On click (host): RPC `AbilityRpcs.GuardianStartShield(targetId, duration)` tints the target with a pulsing green overlay and spawns a pulse ring.
- Visuals:
  - Tint applied via `AbilityCosmetics.ApplyShieldTint` and restored after duration.
  - Pulse ring spawned at target position.
- Defaults:
  - Cooldown: 25s. Shield duration: 6s. Uses: 1.

## Trickster
- Name: Smoke Burst
- Button: `Buttons/TricksterSmokeButton.cs`
- How it works:
  - On click (host): RPC `AbilityRpcs.TricksterStartSmoke(center, radius, duration)` darkens players in a radius around Trickster and spawns a black pulse ring.
- Visuals:
  - Darken applied to SpriteRenderers within radius; restored after duration.
  - Pulse ring spawned at smoke center.
- Defaults:
  - Cooldown: 25s. Initial cooldown: 10s. Duration: 6s. Radius: 3.5.

## Networking
- RPCs implemented with Reactor `MethodRpc` in `Networking/AbilityRpcs.cs` with host authority.
- Buttons only invoke RPCs when `AmongUsClient.Instance.AmHost`.
- Effects end locally after duration using `Reactor.Utilities.Coroutines`.

## Cosmetics Notes
- Current visuals use SpriteRenderer color/alpha as a placeholder.
- Can be migrated to AU cosmetics layer (`CosmeticsLayer` / `CosmeticsUtils`) on request.

## Files
- Buttons: `Buttons/ShadowCloakButton.cs`, `Buttons/GuardianShieldButton.cs`, `Buttons/TricksterSmokeButton.cs`
- RPCs: `Networking/AbilityRpcs.cs`, `Networking/TrollRpcCalls.cs`
- Visual helpers: `Cosmetics/AbilityCosmetics.cs`
- Roles: `Roles/ShadowRole.cs`, `Roles/GuardianRole.cs`, `Roles/TricksterRole.cs`
- Options: `Options/ShadowOptions.cs`, `Options/GuardianOptions.cs`, `Options/TricksterOptions.cs`, `Options/WardenOptions.cs`
