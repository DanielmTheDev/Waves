using Godot;
using Waves.Code.Characters.Projectiles;
using Waves.Code.Characters.Resources;
using Waves.Code.Common;
using Waves.Code.Constants;

namespace Waves.Code.Characters.Players;

public partial class Player : CharacterBody2D
{
    [Export] private CharacterProfile _characterProfile;
    private ProjectileShooter _projectileShooter;
    private Area2D _area2D;

    public override void _Ready()
    {
        _projectileShooter = GetNode<ProjectileShooter>(UniqueNames.ProjectileShooter);
        _area2D = GetNode<Area2D>(UniqueNames.Area2d);
        _area2D.BodyEntered += OnBodyEntered;
        _area2D.AreaEntered += OnBodyEntered;
        AddToGroup(GroupNames.Player);
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
        Velocity = ReadInput().Velocity(_characterProfile.MoveSpeed);
        MoveAndSlide();
        Rotation = GlobalPosition.LookRotation(GetGlobalMousePosition());
    }

    private void OnBodyEntered(Node2D body)
        => body.QueueFree();

    private static Vector2 ReadInput()
        => Input.GetVector("move_left", "move_right", "move_up", "move_down");
}