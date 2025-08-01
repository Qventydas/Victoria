using Robust.Shared.Serialization.TypeSerializers.Implementations.Custom;
namespace Content.Server.Journey
{
    [RegisterComponent]
    public sealed partial class JourneyComponent : Component
    {
        [DataField("nextCheck", customTypeSerializer: typeof(TimeOffsetSerializer)), ViewVariables(VVAccess.ReadWrite)]
        public TimeSpan NextCheck = TimeSpan.Zero;

        [DataField("journeyGroup")]
        public string JourneyGroup = "Alpha";
    }
}
