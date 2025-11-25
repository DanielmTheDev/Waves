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

        UpdateAgentTarget();
        LookTowardsNextTarget();
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

    private void LookTowardsNextTarget()
        => _character.Rotation = _character.GlobalPosition.LookRotation(_agent.GetNextPathPosition());

    private void UpdateAgentTarget()
    {
        if (_agent.TargetPosition != _target.GlobalPosition)
            _agent.TargetPosition = _target.GlobalPosition;
    }
}