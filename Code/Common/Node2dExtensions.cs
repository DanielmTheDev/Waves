using System.Linq;
using Godot;

namespace Waves.Code.Common;

public static class Node2dExtensions
{
    public static void LookTowards(this Node2D node, Vector2 target)
        => node.Rotation = node.GlobalPosition.LookRotation(target);

    public static Node2D NearestTo(this Node2D[] nodes, Node2D origin)
        => nodes.MinBy(hp => hp.GlobalPosition.DistanceTo(origin.GlobalPosition));
}