using Godot;

namespace Waves.Code.Common;

public static class AnimationPlayerExtensions
{
    public static void PlayMovementAnimation(this AnimationPlayer animationPlayer, Vector2 direction, string baseName)
    {
        var animationName = GetAnimationName(baseName, direction);
        if (animationPlayer.CurrentAnimation != animationName)
        {
            animationPlayer.Play(animationName);
        }
    }

    public static void PlayMovementAnimation4Directions(this AnimationPlayer animationPlayer, Vector2 direction, string baseName)
    {
        var animationName = GetAnimationName4Directions(baseName, direction);
        if (animationPlayer.CurrentAnimation != animationName)
        {
            animationPlayer.Play(animationName);
        }
    }

    private static string GetAnimationName(string type, Vector2 direction)
    {
        var angle = direction.Angle();
        var octant = Mathf.FloorToInt((angle + Mathf.Pi / 8f) / (Mathf.Pi / 4f)) & 7;

        return octant switch
        {
            0 => $"{type}_right", // →
            1 => $"{type}_front_right", // ↘
            2 => $"{type}_front", // ↓
            3 => $"{type}_front_left", // ↙
            4 => $"{type}_left", // ←
            5 => $"{type}_back_left", // ↖
            6 => $"{type}_back", // ↑
            7 => $"{type}_back_right", // ↗
            _ => $"{type}_front"
        };
    }

    private static string GetAnimationName4Directions(string type, Vector2 direction)
        => Mathf.Abs(direction.X) > Mathf.Abs(direction.Y)
            ? direction.X > 0
                ? $"{type}_right"
                : $"{type}_left"
            : direction.Y > 0
                ? $"{type}_front"
                : $"{type}_back";
}