using Godot;
using Waves.Code.Common;
using Waves.Code.Enemies.Ranged.Resources;
using Waves.Code.States;
using Waves.Code.World.Combat.CoverPoints;

namespace Waves.Code.Enemies.Ranged.States;

public record HidingContext(CoverPoint[] HidingPoints, CoverPoint Target);

public class GoToHidingPoint : State
{
    private readonly RangedEnemy _character;
    private readonly HidingContext HidingContext;
    private readonly RangedEnemyProfile _profile;
    private readonly NavigationAgent2D _agent;

    public GoToHidingPoint(RangedEnemy character, CoverPoint[] coverPoints, RangedEnemyProfile profile, NavigationAgent2D agent)
    {
        _character = character;
        _profile = profile;
        _agent = agent;
        HidingContext = new HidingContext(coverPoints, coverPoints.NearestTo(character));
    }

    public override void PhysicsUpdate(double delta)
    {
        if (IsAtHidingPoint())
        {
            _character.SwitchToPeeking();
            return;
        }

        _agent.SetVelocityToNextTarget(_character, HidingContext.Target.GlobalPosition, _profile.MoveSpeed);
        _character.LookTowards(_agent.GetNextPathPosition());
        _character.Velocity = _agent.Velocity;
    }

    public override void Exit()
        => _character.Velocity = Vector2.Zero;

    private bool IsAtHidingPoint()
        => HidingContext.Target.GlobalPosition.DistanceTo(_character.GlobalPosition) <= 2;
}