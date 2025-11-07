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

    public override void _Ready()
    {
        _projectileShooter = GetNode<ProjectileShooter>(UniqueNames.ProjectileShooter);
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

    private static Vector2 ReadInput()
        => Input.GetVector("move_left", "move_right", "move_up", "move_down");
}