using Godot;

namespace Waves.Code.Enemies;

public partial class SandboxEnemy : CharacterBody2D
{
    [Export] private Area2D _area2D;

    public override void _Ready()
    {
        _area2D.BodyEntered += OnBodyEntered;
        _area2D.AreaEntered += OnBodyEntered;
    }

    private void OnBodyEntered(Node2D body)
    {
        body.QueueFree();
        QueueFree();
    }
}