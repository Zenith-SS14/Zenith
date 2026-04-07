using System.Text.Json.Serialization;
using Robust.Shared.Serialization;

namespace Content.Shared._Zenith.Events;

[Serializable, NetSerializable]
public sealed class ToggleStabilizerRequest : BoundUserInterfaceMessage
{
    public InertiaDampeningState Mode;
}

[Serializable, NetSerializable]
public enum InertiaDampeningState
{
    Off = 0,
    Dampen = 1,
    Anchored = 2,
}
