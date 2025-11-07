using Godot;
using Waves.Code.Characters.Projectiles;
using Waves.Code.Constants;

namespace Waves.Code.Enemies;

public partial class SandboxEnemy : CharacterBody2D
{
    [Export] public float MoveSpeed = 120f;
    [Export] public float ShootRange = 250f;

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
        var toTarget = _target.GlobalPosition - GlobalPosition;
        var dist = toTarget.Length();

        if (dist > ShootRange)
        {
            var dir = toTarget.Normalized();
            Velocity = dir * MoveSpeed;
            MoveAndSlide();
        }
        else
        {
            Velocity = Vector2.Zero;
            MoveAndSlide();

            _shooter.TryShootAt(_target.GlobalPosition);
        }
    }

    private void OnBodyEntered(Node2D body)
    {
        body.QueueFree();
        QueueFree();
    }
}