using Godot;

namespace Waves.Code.World.Combat.CoverPoints;

public partial class CoverPoint : Node2D
{
	public Node2D ShootingPoint => GetNode<Node2D>(UniqueNames.ShootingPoint);
	public Node2D HidingPoint => GetNode<Node2D>(UniqueNames.HidingPoint);
}
