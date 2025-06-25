namespace Content.Shared.Tackle;

[ByRefEvent]
public record struct CMDisarmEvent(EntityUid User, bool Handled = false);
