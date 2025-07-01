using Content.Server.Administration.Logs;
using Content.Server.Chat.Systems;
using Content.Server.NPC;
using Content.Server.NPC.Systems;
using Content.Shared.Damage;
using Content.Shared.Database;
using Content.Shared.Dragon;
using Content.Shared.Examine;
using Content.Shared.Sprite;
using Robust.Shared.GameObjects;
using Robust.Shared.Map;
using Robust.Shared.Timing;
using Robust.Shared.Player;
using Robust.Shared.Serialization.Manager;
using System.Numerics;
using Robust.Shared.Audio;
using Robust.Shared.Audio.Systems;
using Robust.Shared.Utility;

namespace Content.Server.Journey;

/// <summary>
/// Handles events for rift entities and rift updating.
/// </summary>
public sealed class JourneySystem : EntitySystem
{
    [Dependency] private readonly ChatSystem _chat = default!;
    [Dependency] private readonly IAdminLogManager _adminLog = default!;
    [Dependency] private readonly ISerializationManager _serManager = default!;
    [Dependency] private readonly NPCSystem _npc = default!;
    [Dependency] private readonly IGameTiming _timing = default!;
    [Dependency] private readonly SharedAudioSystem _audio = default!;

    public override void Initialize()
    {
        base.Initialize();
    }

    public override void Update(float frameTime)
    {
        base.Update(frameTime);

        var npc_query = EntityQueryEnumerator<JourneyComponent>();
        while (npc_query.MoveNext(out var uid, out var comp))
        {
            if (_timing.CurTime < comp.NextCheck)
                continue;
            comp.NextCheck = _timing.CurTime + TimeSpan.FromSeconds(1);
            _adminLog.Add(LogType.Action, LogImpact.Low, $"Путешественник-{ToPrettyString(uid)}");

            var targets = EntityQueryEnumerator<JourneyTargetComponent>();
            JourneyTargetComponent fav_target = new JourneyTargetComponent();
			fav_target.Priority = -999;
            float prohodka = 1;

            while (targets.MoveNext(out var targ, out var comp_targ))
            {
                _adminLog.Add(LogType.Action, LogImpact.Low, $"Цель-{ToPrettyString(targ)} с приоритетом(comp_targ.Priority)");
                if (comp_targ.Priority > fav_target.Priority && (comp.JourneyGroup == comp_targ.JourneyGroup))
                    fav_target = comp_targ;
            }
            _npc.SetBlackboard(uid, NPCBlackboard.FollowTarget, new EntityCoordinates(fav_target.Owner, Vector2.Zero));
        }
    }
}
