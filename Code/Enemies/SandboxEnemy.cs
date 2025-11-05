using Godot;

namespace Waves.Code.Enemies;

public partial class SandboxEnemy : Area2D
{
    public override void _Ready()
        => BodyEntered += OnBodyEntered;

    private void OnBodyEntered(Node2D body)
    {
        body.QueueFree();
        QueueFree();
    }
}