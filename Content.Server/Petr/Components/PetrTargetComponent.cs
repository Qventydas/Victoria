namespace Content.Server.Petr.Components;

/// <summary>
/// Given to heads at round start. Used for assigning traitors to kill heads and for revs to check if the heads died or not.
/// </summary>
[RegisterComponent]
public sealed partial class PetrTargetComponent : Component
{

}

//TODO this should probably be on a mind role, not the mob
