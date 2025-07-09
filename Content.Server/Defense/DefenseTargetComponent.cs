namespace Content.Server.Petr.Components;

/// <summary>
/// Given to heads at round start. Used for assigning traitors to kill heads and for revs to check if the heads died or not.
/// </summary>
[RegisterComponent]
public sealed partial class DefenseTargetComponent : Component
{
    /// <summary>
    /// Флаг или нет
    /// </summary>
    [ViewVariables(VVAccess.ReadWrite), DataField("flag")]
    public bool Flag = false;

    /// <summary>
    /// Имя узла.
    /// </summary>
    [ViewVariables(VVAccess.ReadWrite), DataField("name")]
    public string Name = "Подстанция";
}
