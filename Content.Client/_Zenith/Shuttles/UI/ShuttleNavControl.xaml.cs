using Content.Shared._Zenith.Events;
using Content.Shared.Shuttles.Events;
using Content.Shared.Shuttles.BUIStates;
using Robust.Shared.Physics.Components;
using System.Numerics;
using Robust.Client.Graphics;
using Robust.Shared.Collections;

namespace Content.Client.Shuttles.UI
{
    public sealed partial class ShuttleNavControl
    {
        public InertiaDampeningState DampenerState { get; set; }

        private void NfUpdateState(NavInterfaceState state)
        {

            if (!EntManager.GetCoordinates(state.Coordinates).HasValue ||
                !EntManager.TryGetComponent(EntManager.GetCoordinates(state.Coordinates).GetValueOrDefault().EntityId,
                    out TransformComponent? transform) ||
                !EntManager.TryGetComponent(transform.GridUid, out PhysicsComponent? physicsComponent))
            {
                return;
            }

            DampenerState = physicsComponent.LinearDamping == 0 ? InertiaDampeningState.Off :
                physicsComponent.AngularDamping < .1 ? InertiaDampeningState.Dampen :
                InertiaDampeningState.Anchored;

        }
    }
}
