using Godot;

namespace Waves.Code.Common;

public static class Node2dExtensions
{
    public static void LookTowards(this Node2D node, Vector2 target)
        => node.Rotation = node.GlobalPosition.LookRotation(target);

    public static bool CanSee(this Node2D self, Node2D target, uint collisionMask = uint.MaxValue)
    {
        var query = PhysicsRayQueryParameters2D.Create(self.GlobalPosition, target.GlobalPosition);
        query.CollisionMask = collisionMask;
        if (self is CollisionObject2D physicsSelf)
        {
            query.Exclude = [physicsSelf.GetRid()];
        }

        var result = self.GetWorld2D().DirectSpaceState.IntersectRay(query);
        return result.Count > 0 && result["collider"].As<Node2D>() == target;
    }

    public static bool CanClearPath(this Node2D self, Node2D target, float radius, uint collisionMask = uint.MaxValue)
    {
        var query = new PhysicsShapeQueryParameters2D
        {
            Shape = new CircleShape2D { Radius = radius },
            Transform = new Transform2D(0, self.GlobalPosition),
            Motion = self.GlobalPosition.VectorTo(target.GlobalPosition),
            CollisionMask = collisionMask
        };

        if (self is CollisionObject2D physicsSelf)
        {
            query.Exclude = [physicsSelf.GetRid()];
        }

        // CastMotion returns [safe_fraction, unsafe_fraction]
        // safe_fraction = 0.0 (stuck immediately) to 1.0 (moved full distance)
        var result = self.GetWorld2D().DirectSpaceState.CastMotion(query);
        return result is { Length: > 0 } && result[0] >= 0.99f;
    }
}
