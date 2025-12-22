using System.Collections.Generic;
using System.Linq;
using Godot;

namespace Waves.Code.Common;

public static class NodeExtensions
{
    public static IEnumerable<T> GetNodesInGroup<T>(this Node node, string groupName)
        where T : Node
        => node.GetTree().GetNodesInGroup(groupName).OfType<T>().ToArray();
}