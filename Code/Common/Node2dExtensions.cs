using Godot;

namespace Waves.Code.Common;

public static class Node2dExtensions
{
    public static void LookTowards(this Node2D node, Vector2 target)
        => node.Rotation = node.GlobalPosition.LookRotation(target);
}