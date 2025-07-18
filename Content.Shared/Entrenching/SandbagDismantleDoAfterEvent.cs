using Content.Shared.DoAfter;
using Robust.Shared.Map;
using Robust.Shared.Serialization;

namespace Content.Shared.Entrenching;

[Serializable, NetSerializable]
public sealed partial class SandbagDismantleDoAfterEvent : SimpleDoAfterEvent
{
    [DataField(required: true)]
    public NetCoordinates Coordinates;

    public SandbagDismantleDoAfterEvent(NetCoordinates coordinates)
    {
        Coordinates = coordinates;
    }
}
