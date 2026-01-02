using System.Linq;
using Godot;
using Waves.Code.Constants;
using Waves.Code.World.Combat.CoverPoints;

namespace Waves.Code.Infrastructure;

public sealed partial class CoverPoints : Node
{
    public static CoverPoints Instance { get; private set; }

    public override void _EnterTree()
        => Instance = this;

    public CoverPoint[] All()
        => GetTree().GetNodesInGroup(GroupNames.CoverPoint).OfType<CoverPoint>().ToArray();

    public CoverPoint Random()
        => All().OrderBy(_ => GD.Randf()).First();

    public CoverPoint Nearest(Node2D origin)
        => All().NearestTo(origin);
}