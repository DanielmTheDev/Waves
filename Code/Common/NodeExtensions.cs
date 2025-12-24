using System.Collections.Generic;
using System.Linq;
using Godot;
using Waves.Code.Constants;

namespace Waves.Code.Common;

public static class NodeExtensions
{
    public static IEnumerable<T> GetNodesInGroup<T>(this Node node, string groupName)
        where T : Node
        => node.GetTree().GetNodesInGroup(groupName).OfType<T>().ToArray();

    public static World.Combat.CoverPoints.CoverPoint[] AllCoverpoints(this Node node)
        => node.GetTree().GetNodesInGroup(GroupNames.CoverPoint).OfType<World.Combat.CoverPoints.CoverPoint>().ToArray();
}