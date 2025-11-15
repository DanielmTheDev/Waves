using Godot;
using Waves.Code.Common;
using Waves.Code.Constants;
using Waves.Code.Players.Projectiles;

namespace Waves.Code.Enemies.Ranged;

public partial class RangedEnemy : CharacterBody2D
{
    [Export] public Resources.RangedEnemyProfile Profile { get; set; }

    private Area2D _area2D => GetNode<Area2D>(UniqueNames.Area2d);
    private NavigationAgent2D _agent => GetNode<NavigationAgent2D>(UniqueNames.NavigationAgent2d);
    private ProjectileShooter _shooter => GetNode<ProjectileShooter>(UniqueNames.ProjectileShooter);

    private Node2D _target;

    public override void _Ready()
    {
        AddToGroup(GroupNames.Enemy);
        _target = GetTree().GetFirstNodeInGroup(GroupNames.Player) as Node2D;
        _agent.AvoidanceEnabled = true;
        _agent.VelocityComputed += OnVelocityComputed;
        _agent.DebugEnabled = true;
        _area2D.BodyEntered += OnBodyEntered;
        _area2D.AreaEntered += OnBodyEntered;
    }

    public override void _PhysicsProcess(double delta)
    {
        // todo: this should look towards next path of agent
        var toTarget = _target.GlobalPosition - GlobalPosition;
        Rotation = GlobalPosition.LookRotation(_target.GlobalPosition);

        if (toTarget.Length() > Profile.ShootRange)
        {
            UpdateAgentTarget();
            SteerAlongPath();
        }
        else
        {
            Halt();
            _shooter.TryShootAt(_target.GlobalPosition);
        }
    }

    private void UpdateAgentTarget()
    {
        if (_agent.TargetPosition != _target.GlobalPosition)
            _agent.TargetPosition = _target.GlobalPosition;
    }

    private void SteerAlongPath()
    {
        if (_agent.IsNavigationFinished())
        {
            Halt();
            return;
        }

        _agent.TargetPosition = _target.GlobalPosition;

        var next = _agent.GetNextPathPosition();
        var desired = (next - GlobalPosition).Normalized() * Profile.MoveSpeed;
        _agent.SetVelocity(desired);
    }

    private void Halt()
    {
        Velocity = Vector2.Zero;
        MoveAndSlide();
    }

    private void OnBodyEntered(Node2D body)
    {
        body.QueueFree();
        QueueFree();
    }

    private void OnVelocityComputed(Vector2 safeVelocity)
    {
        Velocity = safeVelocity;
        MoveAndSlide();
    }
}