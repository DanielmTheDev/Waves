using Godot;
using Waves.Code.Characters.Core;

namespace Waves.Code.Characters.Players;

public partial class Player : CharacterBody2D
{
    [Export] public float MoveSpeed = 200f;

    public override void _PhysicsProcess(double delta)
    {
        var inputDir = ReadInput();
        Velocity = MovementLogic.ComputeVelocity(inputDir, MoveSpeed);
        MoveAndSlide();

        var mousePos = GetGlobalMousePosition();
        Rotation = AimLogic.ComputeLookRotation(GlobalPosition, mousePos);
    }

    private static Vector2 ReadInput()
        => Input.GetVector("move_left", "move_right", "move_up", "move_down");
}