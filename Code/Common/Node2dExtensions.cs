using Godot;

namespace Waves.Code.Common;

public static class Node2dExtensions
{
    public static void LookTowards(this Node2D node, Vector2 target)
        => node.Rotation = node.GlobalPosition.LookRotation(target);

    public static bool CanSee(this Node2D self, Node2D target, uint collisionMask = uint.MaxValue)
    {
        var spaceState = self.GetWorld2D().DirectSpaceState;
        var query = PhysicsRayQueryParameters2D.Create(self.GlobalPosition, target.GlobalPosition);

        query.CollisionMask = collisionMask;

        if (self is CollisionObject2D physicsSelf)
        {
            query.Exclude = [physicsSelf.GetRid()];
        }

        var result = spaceState.IntersectRay(query);

        if (result.Count > 0)
        {
            var collider = result["collider"].As<Node2D>();
            return collider == target;
        }

        return false;
    }
}
