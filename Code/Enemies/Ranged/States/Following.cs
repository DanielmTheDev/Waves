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

    public override void Enter()
    {
    }

    public override void Update(double delta)
    {
    }

    public override void PhysicsUpdate(double delta)
    {
        var distance = _target.GlobalPosition.DistanceTo(_character.GlobalPosition);
        if (distance <= _profile.ShootRange)
        {
            _character.SwitchToShooting();
            return;
        }

        _agent.UpdateAgentTarget(_target);
        _character.LookTowards(_agent.GetNextPathPosition());
        GetVelocityToNextTarget();
        _character.Velocity = GetVelocityToNextTarget();
    }

    private Vector2 GetVelocityToNextTarget()
    {
        if (_agent.IsNavigationFinished())
        {
            return Vector2.Zero;
        }

        _agent.TargetPosition = _target.GlobalPosition;
        var next = _agent.GetNextPathPosition();
        var desired = (next - _character.GlobalPosition).Normalized() * _profile.MoveSpeed;
        _agent.SetVelocity(desired);
        return _agent.Velocity;
    }
}