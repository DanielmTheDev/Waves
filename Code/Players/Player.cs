using Godot;
using Waves.Code.Common;
using Waves.Code.Constants;
using Waves.Code.Infrastructure;
using Waves.Code.Players.Projectiles;
using Waves.Code.Players.Resources;

namespace Waves.Code.Players;

public record Hitpoints(int Current, int Max);

public partial class Player : CharacterBody2D
{
    [Export] public CharacterProfile CharacterProfile;
    private ProjectileShooter _projectileShooter;
    private Area2D _area2D;
    private Hitpoints _hitPoints;

    public override void _Ready()
    {
        AddToGroup(GroupNames.Player);
        _hitPoints = new(CharacterProfile.HitPoints, CharacterProfile.HitPoints);
        _projectileShooter = GetNode<ProjectileShooter>(UniqueNames.ProjectileShooter);
        _area2D = GetNode<Area2D>(UniqueNames.Area2d);
        _area2D.BodyEntered += OnBodyEntered;
        _area2D.AreaEntered += OnBodyEntered;
    }

    public override void _PhysicsProcess(double delta)
    {
        ProcessMovement();
        ProcessAttack();
    }

    private void ProcessAttack()
    {
        if (!Input.IsActionJustPressed("shoot"))
            return;
        _projectileShooter.TryShootAt(GetGlobalMousePosition());
    }

    private void ProcessMovement()
    {
        Velocity = ReadInput().Velocity(CharacterProfile.MoveSpeed);
        MoveAndSlide();
    }

    private void OnBodyEntered(Node2D body)
    {
        _hitPoints = _hitPoints with { Current = _hitPoints.Current - 1 };
        EventBus.Instance.EmitHitPointChanged(_hitPoints.Current, _hitPoints.Max);
        body.QueueFree();
    }

    private static Vector2 ReadInput()
        => Input.GetVector("move_left", "move_right", "move_up", "move_down");
}