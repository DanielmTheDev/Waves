using System.Linq;
using Godot;

namespace Waves.Code.World.Combat.CoverPoints;

public static class Extensions
{
    public static CoverPoint NearestTo(this CoverPoint[] nodes, Node2D origin)
        => nodes.MinBy(hp => hp.GlobalPosition.DistanceTo(origin.GlobalPosition));
}