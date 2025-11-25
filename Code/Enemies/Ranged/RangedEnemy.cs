using Godot;
using Waves.Code.Constants;
using Waves.Code.Enemies.Ranged.Resources;
using Waves.Code.Enemies.Ranged.States;
using Waves.Code.Players.Projectiles;
using Waves.Code.States;

namespace Waves.Code.Enemies.Ranged;

public partial class RangedEnemy : CharacterBody2D
{
    [Export] public RangedEnemyProfile Profile { get; set; }
    private Area2D _area2D => GetNode<Area2D>(UniqueNames.Area2d);
    private NavigationAgent2D _agent => GetNode<NavigationAgent2D>(UniqueNames.NavigationAgent2d);
    private ProjectileShooter _shooter => GetNode<ProjectileShooter>(UniqueNames.ProjectileShooter);

    private State _state;
    private Node2D _target;

    public override void _Ready()
    {
        AddToGroup(GroupNames.Enemy);
        _target = GetTree().GetFirstNodeInGroup(GroupNames.Player) as Node2D;
        _area2D.BodyEntered += OnBodyEntered;
        _area2D.AreaEntered += OnBodyEntered;
        _state = new Following(this, _target, Profile, _agent);
        _state.Enter();
    }

    public override void _PhysicsProcess(double delta)
    {
        _state.PhysicsUpdate(delta);
        MoveAndSlide();
    }

    private void OnBodyEntered(Node2D body)
    {
        body.QueueFree();
        QueueFree();
    }

    public void SwitchToShooting()
        => SwitchState(new Shooting(this, _target, _shooter));

    public void SwitchToFollowing()
        => SwitchState(new Following(this, _target, Profile, _agent));

    private void SwitchState(State state)
    {
        _state.Exit();
        _state = state;
        _state.Enter();
    }
}