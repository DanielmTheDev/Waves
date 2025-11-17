using Godot;
using Waves.Code.Common;
using Waves.Code.Enemies.Ranged.Resources;

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
        var distance = _target.GlobalPosition.DistanceTo(_character.GlobalPosition);
        if (distance <= _profile.ShootRange)
        {
            _character.SwitchToShooting();
            return;
        }

        UpdateAgentTarget();
        LookTowardsNextTarget();
        GetVelocityToNextTarget(delta);
        _character.Velocity = GetVelocityToNextTarget(delta);
        _character.MoveAndSlide();
    }

    public override void Exit()
    {
    }

    private Vector2 GetVelocityToNextTarget(double delta)
    {
        if (_agent.IsNavigationFinished())
        {
            return Vector2.Zero;
        }

        _agent.TargetPosition = _target.GlobalPosition;

        var next = _agent.GetNextPathPosition();
        var desired = (next - _character.GlobalPosition).Normalized() * _profile.MoveSpeed * (float)delta;
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