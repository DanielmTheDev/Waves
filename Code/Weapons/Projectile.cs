using Godot;

namespace Waves.Code.Weapons;

public partial class Projectile : RigidBody2D
{
	public override void _Ready()
		=> BodyEntered += OnBodyEntered;

	private void OnBodyEntered(Node body)
		=> QueueFree();
}
