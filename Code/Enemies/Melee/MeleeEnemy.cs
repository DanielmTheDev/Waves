using Godot;
using Waves.Code.Constants;
using Waves.Code.Enemies.Melee.States;
using Waves.Code.States;
using Following = Waves.Code.Enemies.Melee.States.Following;

namespace Waves.Code.Enemies.Melee;

public partial class MeleeEnemy : CharacterBody2D
{
    [Export] public Resources.MeleeEnemyProfile Profile { get; set; }

    private Area2D _area2D => GetNode<Area2D>(UniqueNames.Area2d);
    private NavigationAgent2D _agent => GetNode<NavigationAgent2D>(UniqueNames.NavigationAgent2d);

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
        => _state.PhysicsUpdate(delta);

    private void OnBodyEntered(Node2D body)
    {
        body.QueueFree();
        QueueFree();
    }

    public void SwitchToAttacking()
        => SwitchState(new Attacking(_target, this));

    public void SwitchToFollowing()
        => SwitchState(new Following(this, _target, Profile, _agent));

    private void SwitchState(State state)
    {
        _state.Exit();
        _state = state;
        _state.Enter();
    }
}