namespace Content.Server.Journey
{
    [RegisterComponent]
    public sealed partial class JourneyTargetComponent : Component
    {
        [DataField("prioryti")]
        public int Priority = 1;

        [DataField("ignoreGroups")]
        public bool IgnoreGroups = false;

        [DataField("journeyGroup")]
        public string JourneyGroup = "Alpha";
    }
}
