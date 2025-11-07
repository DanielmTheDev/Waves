using Godot;

namespace Waves.Code.Common;

public static class VectorExtensions
{
    public static Vector2 VectorTo(this Vector2 origin, Vector2 target)
        => target - origin;

    public static Vector2 NormalizedTo(this Vector2 origin, Vector2 target)
        => origin.VectorTo(target).Normalized();

    public static float LookRotation(this Vector2 origin, Vector2 target)
        => (target - origin).Angle();

    public static Vector2 Velocity(this Vector2 direction, float speed)
        => direction * speed;
}