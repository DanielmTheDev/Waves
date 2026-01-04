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
        => AllFree().Length == 0
            ? None
            : Some(AllFree()[GD.RandRange(0, AllFree().Length - 1)]);

    public Option<CoverPoint> FreeNearestToPlayer(float maxDistance = 600f)
    {
        var player = (Node2D)GetTree().GetFirstNodeInGroup(GroupNames.Player);
        var freeNodes = AllFree()
            .Where(n => n.HidingPoint.GlobalPosition.DistanceTo(player.GlobalPosition) <= maxDistance)
            .OrderBy(n => n.HidingPoint.GlobalPosition.DistanceTo(player.GlobalPosition))
            .ToArray();

        return freeNodes.Length() == 0
            ? None
            : Some(freeNodes.First());
    }
}