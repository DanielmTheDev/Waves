using Godot;
using Waves.Code.Characters.Core;
using Waves.Code.Characters.Resources;

namespace Waves.Code.Characters.Players;

public partial class Player : CharacterBody2D
{
    [Export] private CharacterProfile _characterProfile;

    public override void _PhysicsProcess(double delta)
    {
        var inputDir = ReadInput();
        Velocity = MovementLogic.ComputeVelocity(inputDir, _characterProfile.MoveSpeed);
        MoveAndSlide();

        var mousePos = GetGlobalMousePosition();
        Rotation = AimLogic.ComputeLookRotation(GlobalPosition, mousePos);
    }

    private static Vector2 ReadInput()
        => Input.GetVector("move_left", "move_right", "move_up", "move_down");
}