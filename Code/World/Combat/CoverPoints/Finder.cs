using System.Linq;
using Godot;
using Waves.Code.Constants;

namespace Waves.Code.World.Combat.CoverPoints;

public sealed partial class Finder : Node
{
    public static Finder Instance { get; private set; }

    public override void _EnterTree()
        => Instance = this;

    private CoverPoint[] All()
        => GetTree().GetNodesInGroup(GroupNames.CoverPoint).OfType<CoverPoint>().ToArray();

    public CoverPoint[] Free()
        => All().Where(cp => !cp.IsOccupied).ToArray();

    public CoverPoint RandomFree()
        => All().OrderBy(_ => GD.Randf()).First();

    public CoverPoint Nearest(Node2D origin)
        => All().NearestTo(origin);
}