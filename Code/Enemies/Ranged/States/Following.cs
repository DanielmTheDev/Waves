using Godot;
using Waves.Code.Common;
using Waves.Code.Enemies.Ranged.Resources;
using Waves.Code.States;

namespace Waves.Code.Enemies.Ranged.States;

public class Following : State
{
    private readonly RangedEnemy _character;
    private readonly Node2D _target;
    private readonly RangedEnemyProfile _profile;
    private readonly NavigationAgent2D _agent;

    public Following(RangedEnemy character, Node2D target, RangedEnemyProfile profile, NavigationAgent2D agent)
    {
        _character = character;
        _target = target;
        _profile = profile;
        _agent = agent;
    }

    public override void PhysicsUpdate(double delta)
    {
        var distance = _target.GlobalPosition.DistanceTo(_character.GlobalPosition);
        if (distance <= _profile.ShootRange)
        {
            _character.SwitchToShooting();
            return;
        }

        _agent.SetVelocityToNextTarget(_target.GlobalPosition, _profile.MoveSpeed);
        _character.LookTowards(_agent.GetNextPathPosition());
        _character.Velocity = _agent.Velocity;
    }
}