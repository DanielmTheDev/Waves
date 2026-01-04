using System.Linq;
using Godot;
using LanguageExt;
using static LanguageExt.Prelude;
using Waves.Code.Constants;

namespace Waves.Code.World.Combat.CoverPoints;

public sealed partial class Finder : Node
{
    public static Finder Instance { get; private set; }

    public override void _EnterTree()
        => Instance = this;

    private CoverPoint[] All()
        => GetTree().GetNodesInGroup(GroupNames.CoverPoint).OfType<CoverPoint>().ToArray();

    private CoverPoint[] AllFree()
        => All().Where(cp => !cp.IsOccupied).ToArray();

    public CoverPoint Nearest(Node2D origin)
        => All().NearestTo(origin);

    public Option<CoverPoint> RandomFree()
    {
        var available = AllFree();
        return available.Length == 0
            ? None
            : Some(available[GD.RandRange(0, available.Length - 1)]);
    }
}