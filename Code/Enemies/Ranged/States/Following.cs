using Godot;
using Waves.Code.Common;
using Waves.Code.Enemies.Ranged.Resources;
using Waves.Code.States;

namespace Waves.Code.Enemies.Ranged.States;

public record HidingContext(Node2D[] HidingPoints, Node2D Target);

public class Following : State
{
    private readonly RangedEnemy _character;
    private readonly HidingContext HidingContext;
    private readonly RangedEnemyProfile _profile;
    private readonly NavigationAgent2D _agent;

    public Following(RangedEnemy character, Node2D[] hidingPoints, RangedEnemyProfile profile, NavigationAgent2D agent)
    {
        _character = character;
        _profile = profile;
        _agent = agent;
        HidingContext = new HidingContext(hidingPoints, hidingPoints.NearestTo(character));
    }

    public override void PhysicsUpdate(double delta)
    {
        var distance = HidingContext.Target.GlobalPosition.DistanceTo(_character.GlobalPosition);
        if (distance <= 2)
        {
            _character.SwitchToShooting();
            return;
        }

        _agent.SetVelocityToNextTarget(_character, HidingContext.Target.GlobalPosition, _profile.MoveSpeed);
        _character.LookTowards(_agent.GetNextPathPosition());
        _character.Velocity = _agent.Velocity;
    }

    public override void Exit()
        => _character.Velocity = Vector2.Zero;
}