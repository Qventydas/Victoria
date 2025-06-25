using Robust.Shared.GameStates;

namespace Content.Shared.Entrenching;

[RegisterComponent, NetworkedComponent]
[Access(typeof(BarricadeSystem))]
public sealed partial class BarricadeComponent : Component;
