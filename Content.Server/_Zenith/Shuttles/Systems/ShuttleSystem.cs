using Content.Server.Shuttles.Components;
using Content.Shared.Shuttles.Systems;
using Robust.Shared.Physics.Components;
using Content.Shared._Zenith.Events;
using Robust.Shared.GameObjects;
using System.Numerics;


namespace Content.Server.Shuttles.Systems;

public sealed partial class ShuttleSystem
{
    // Inertial dampening system based on New Frontiers implementation, thanks guys!
    private const float SpaceFrictionStrength = 0.0075f;
    private const float DampenDampingStrength = 0.25f;
    private const float AnchorDampingStrength = 3f;

    private void OnToggleStabilizer(EntityUid uid, ShuttleConsoleComponent component, ref ToggleStabilizerRequest args)
    {
        if (!TryComp(uid, out TransformComponent? transform) ||
            !transform.GridUid.HasValue ||
            !TryComp(transform.GridUid, out PhysicsComponent? physicsComponent) ||
            !TryComp(transform.GridUid, out ShuttleComponent? shuttleComponent))
        {
            return;
        }

        var dampeningStrength = args.Mode switch
        {
            InertiaDampeningState.Off => SpaceFrictionStrength,
            InertiaDampeningState.Dampen => DampenDampingStrength,
            InertiaDampeningState.Anchored => AnchorDampingStrength,
            _ => DampenDampingStrength,
        };

        shuttleComponent.BodyModifier = dampeningStrength;

        if (shuttleComponent.DampingModifier != 0)
            shuttleComponent.DampingModifier = dampeningStrength;
    }
}
