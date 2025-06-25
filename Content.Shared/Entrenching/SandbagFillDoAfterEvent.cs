using Content.Shared.DoAfter;
using Robust.Shared.Serialization;

namespace Content.Shared.Entrenching;

[Serializable, NetSerializable]
public sealed partial class SandbagFillDoAfterEvent : SimpleDoAfterEvent;
