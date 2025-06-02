using Robust.Shared.GameStates;

namespace Content.Shared.RandomChangeTime;

[NetworkedComponent]
[RegisterComponent]
public sealed partial class RandomChangeTimeComponent : Component
{
    [DataField]
    public float Time = 300f;

    [DataField]
    public float Prob = 0.5f;

    [DataField]
    public string? Entity;

    [DataField]
    public float NextCheckTime;
}