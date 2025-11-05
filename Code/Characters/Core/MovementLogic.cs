using Godot;

namespace Waves.Code.Characters.Core;

public static class MovementLogic
{
    public static Vector2 ComputeVelocity(Vector2 inputDir, float speed)
        => inputDir * speed;
}