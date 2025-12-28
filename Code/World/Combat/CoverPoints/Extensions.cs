using System.Linq;
using Godot;

namespace Waves.Code.World.Combat.CoverPoints;

public static class Extensions
{
    public static CoverPoint NearestTo(this CoverPoint[] coverPoints, Node2D origin)
        => coverPoints.MinBy(cp => cp.GlobalPosition.DistanceTo(origin.GlobalPosition));
}