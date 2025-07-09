namespace Content.Server.Petr.Components;

/// <summary>
/// Given to heads at round start. Used for assigning traitors to kill heads and for revs to check if the heads died or not.
/// </summary>
[RegisterComponent]
public sealed partial class DefenseSettingsComponent : Component
{
    /// <summary>
    /// Стартовое вол-во узлов
    /// </summary>
    [ViewVariables(VVAccess.ReadWrite), DataField("startNodes")]
    public int StartNodes = 4;
}
