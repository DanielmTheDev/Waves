using Godot;
using Waves.Code.Constants;

namespace Waves.Code.Enemies;

public partial class SandboxEnemy : CharacterBody2D
{
    [Export] private Area2D _area2D; // todo: dont export
    [Export] public float MoveSpeed = 120f;
    private Node2D _target;

    public override void _Ready()
    {
        _target = GetTree().GetFirstNodeInGroup(GroupNames.Player) as Node2D;
        _area2D.BodyEntered += OnBodyEntered;
        _area2D.AreaEntered += OnBodyEntered;
    }

    public override void _PhysicsProcess(double delta)
    {
        var dir = (_target.GlobalPosition - GlobalPosition).Normalized();
        Velocity = dir * MoveSpeed;
        MoveAndSlide();
    }

    private void OnBodyEntered(Node2D body)
    {
        body.QueueFree();
        QueueFree();
    }
}