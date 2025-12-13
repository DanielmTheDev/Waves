using Godot;
using Waves.Code.Common;
using Waves.Code.Enemies.Melee.Resources;
using Waves.Code.States;

namespace Waves.Code.Enemies.Melee.States;

public class Following : State
{
    private readonly MeleeEnemy _character;
    private readonly Node2D _target;
    private readonly MeleeEnemyProfile _profile;
    private readonly NavigationAgent2D _agent;

    public Following(MeleeEnemy character, Node2D target, MeleeEnemyProfile profile, NavigationAgent2D agent)
    {
        _character = character;
        _target = target;
        _profile = profile;
        _agent = agent;
    }

    public override void PhysicsUpdate(double delta)
    {
        var distance = _target.GlobalPosition.DistanceTo(_character.GlobalPosition);
        if (distance <= _profile.Range)
        {
            _character.SwitchToAttacking();
            return;
        }

        _agent.SetVelocityToNextTarget(_character, _target.GlobalPosition, _profile.MoveSpeed);
        _character.LookTowards(_agent.GetNextPathPosition());
        _character.Velocity = _agent.Velocity;
    }
}