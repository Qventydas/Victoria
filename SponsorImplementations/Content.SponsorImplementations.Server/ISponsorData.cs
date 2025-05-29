using Robust.Shared.Maths;
using Robust.Shared.Network;
using Robust.Shared.Serialization.Manager.Attributes;

namespace Content.SponsorImplementations.Server;

public interface ISponsorDataProvider
{
    public ISponsorData? GetSponsorInfo(NetUserId userId);
}


public interface ISponsorData
{
    public Guid Guid {get;}
    public List<string> Prototypes {get;}
    public Color? Color {get;}
    public int ExtraCharSlots {get;}
    public bool ServerPriorityJoin { get; }

    public void CopyFrom(ISponsorData other);
}

[DataDefinition]
public sealed partial class SponsorData: ISponsorData
{
    [DataField] public Guid Guid { get; set; }
    [DataField] public List<string> Prototypes { get; set; }  = new();
    [DataField] public Color? Color { get; set; }
    [DataField] public int ExtraCharSlots { get; set; }
    [DataField] public bool ServerPriorityJoin { get; set;}

    public void CopyFrom(ISponsorData other)
    {
        Guid = other.Guid;
        Prototypes = other.Prototypes;
        Color = other.Color;
        ExtraCharSlots = other.ExtraCharSlots;
        ServerPriorityJoin = other.ServerPriorityJoin;
    }
}
