using Godot;

namespace Waves.Code.World.Combat.CoverPoints;

public partial class CoverPoint : Node2D
{
	public bool IsOccupied { get; private set; }
	public Node2D ShootingPoint => GetNode<Node2D>(UniqueNames.ShootingPoint);
	public Node2D HidingPoint => GetNode<Node2D>(UniqueNames.HidingPoint);
	public void Occupy()
		=> IsOccupied = true;
	public void Free()
		=> IsOccupied = false;
}
