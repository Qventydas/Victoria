using Content.Server.Administration.Logs;
using Content.Server.Antag;
using Content.Server.EUI;
using Content.Server.Flash;
using Content.Server.GameTicking.Rules.Components;
using Content.Server.Mind;
using Content.Server.Popups;
using Content.Server.Petr.Components;
using Content.Server.Roles;
using Content.Server.RoundEnd;
using Content.Server.Shuttles.Systems;
using Content.Server.Station.Systems;
using Content.Shared.Database;
using Content.Shared.GameTicking.Components;
using Content.Shared.Humanoid;
using Content.Shared.IdentityManagement;
using Content.Shared.Mind.Components;
using Content.Shared.Mindshield.Components;
using Content.Shared.Mobs;
using Content.Shared.Mobs.Components;
using Content.Shared.Mobs.Systems;
using Content.Shared.NPC.Prototypes;
using Content.Shared.NPC.Systems;
using Robust.Shared.Prototypes;
using Robust.Shared.Timing;
using Content.Shared.Cuffs.Components;
using Content.Shared.Destructible;

namespace Content.Server.GameTicking.Rules;

/// <summary>
/// Система для защиты
/// </summary>
public sealed class DefenseRuleSystem : GameRuleSystem<DefenseRuleComponent>
{
    [Dependency] private readonly AntagSelectionSystem _antag = default!;
    [Dependency] private readonly IAdminLogManager _adminLog = default!;
    [Dependency] private readonly EmergencyShuttleSystem _emergencyShuttle = default!;
    [Dependency] private readonly RoleSystem _role = default!;
    [Dependency] private readonly RoundEndSystem _roundEnd = default!;
    [Dependency] private readonly StationSystem _stationSystem = default!;
    [Dependency] private readonly IGameTiming _timing = default!;

    private int _startDefenseNodes = 0;
    private int _defenseNodes = 0;
    private bool _end = false;
    private int _enemyKilled = 0;
    public override void Initialize()
    {
        base.Initialize();
        SubscribeLocalEvent<DefenseTargetComponent, DestructionEventArgs>(OnTargetDestroyed);

        SubscribeLocalEvent<DefenseEnemyComponent, MobStateChangedEvent>(OnBossDeath);

        //SubscribeLocalEvent<PetrRoleComponent, GetBriefingEvent>(OnGetBriefing);

    }

    protected override void ActiveTick(EntityUid uid, DefenseRuleComponent component, GameRuleComponent gameRule, float frameTime)
    {
        base.ActiveTick(uid, component, gameRule, frameTime);
        if (component.NextCheck <= _timing.CurTime)
        {
            component.NextCheck = _timing.CurTime + component.TimerWait;
            if (_end)
            {
                _roundEnd.EndRound();
                GameTicker.EndGameRule(uid, gameRule);
            }
        }
    }
    protected override void Started(EntityUid uid, DefenseRuleComponent component, GameRuleComponent gameRule, GameRuleStartedEvent args)
    {
        base.Started(uid, component, gameRule, args);
        _adminLog.Add(LogType.Action, LogImpact.Extreme, $"СТАРТУЕМ!!!");
        component.NextCheck = _timing.CurTime + component.TimerWait;
        var targets = AllEntityQuery<DefenseSettingsComponent>();
        while (targets.MoveNext(out var id, out var comp))
        {
            _startDefenseNodes = comp.StartNodes;
        }
        _adminLog.Add(LogType.Action, LogImpact.Extreme, $"Кол-во узлов обороны- {_startDefenseNodes}");
        _defenseNodes = _startDefenseNodes;
    }

    protected override void AppendRoundEndText(EntityUid uid,
        DefenseRuleComponent component,
        GameRuleComponent gameRule,
        ref RoundEndTextAppendEvent args)
    {
        base.AppendRoundEndText(uid, component, gameRule, ref args);
        args.AddLine($"Осталось {_defenseNodes} узлов из {_startDefenseNodes}.");
        args.AddLine($"Было уничтожено {_enemyKilled} ксеносов.");
        _end = false;
        _enemyKilled = 0;
    }

    /*         private void OnGetBriefing(EntityUid uid, PetrRoleComponent comp, ref GetBriefingEvent args)
            {
                var ent = args.Mind.Comp.OwnedEntity;
                var head = HasComp<PetrOperativeComponent>(ent);
                args.Append(Loc.GetString("petr-briefing"));
            }
         */
    private void OnTargetDestroyed(EntityUid uid, DefenseTargetComponent comp, DestructionEventArgs args)
    {
        _defenseNodes -= 1;
        _adminLog.Add(LogType.Action, LogImpact.Extreme, $"Мы проебали узел обороны! Осталось {_defenseNodes}");
        if (comp.Flag)
            _end = true;
    }
    private void OnBossDeath(EntityUid uid, DefenseEnemyComponent comp, MobStateChangedEvent ev)
    {
        if (ev.NewMobState == MobState.Dead || ev.NewMobState == MobState.Critical)
        {
            _enemyKilled += 1;
            if (comp.Boss)
                _end = true;
        }


    }
    private static readonly string[] Outcomes =
{
        //Потеряно всё
        "def-remain-nothing",
        // Остался только флаг
        "def-remain-flag",
        //Осталась четверть
        "def-remain-25",
        //Осталась половина
        "def-remain-50",
        //Потеряно 3 четверти
        "def-remain-75",
        //Ничего не потеряно
        "def-remain-100",
    };
}
