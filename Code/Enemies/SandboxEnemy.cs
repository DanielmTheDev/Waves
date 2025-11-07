using Godot;
using Waves.Code.Characters.Projectiles;
using Waves.Code.Common;
using Waves.Code.Constants;

namespace Waves.Code.Enemies;

public partial class SandboxEnemy : CharacterBody2D
{
    [Export] public SandboxEnemyProfile Profile { get; set; }

    private Area2D _area2D => GetNode<Area2D>(UniqueNames.Area2d);
    private ProjectileShooter _shooter => GetNode<ProjectileShooter>(UniqueNames.ProjectileShooter);
    private Node2D _target;

    public override void _Ready()
    {
        _target = GetTree().GetFirstNodeInGroup(GroupNames.Player) as Node2D;
        _area2D.BodyEntered += OnBodyEntered;
        _area2D.AreaEntered += OnBodyEntered;
    }

    public override void _PhysicsProcess(double delta)
    {
        Rotation = GlobalPosition.LookRotation(_target.GlobalPosition);
        if (GlobalPosition.DistanceTo(_target.GlobalPosition) > Profile.ShootRange)
        {
            MoveTowardsTarget();
        }
        else
        {
            TryShoot();
        }
    }

    private void MoveTowardsTarget()
    {
        var dir = GlobalPosition.NormalizedTo(_target.GlobalPosition);
        Velocity = dir * Profile.MoveSpeed;
        MoveAndSlide();
    }

    private void TryShoot()
    {
        Velocity = Vector2.Zero;
        MoveAndSlide();
        _shooter.TryShootAt(_target.GlobalPosition);
    }

    private void OnBodyEntered(Node2D body)
    {
        body.QueueFree();
        QueueFree();
    }
}