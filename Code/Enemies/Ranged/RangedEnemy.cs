using System.Linq;
using Godot;
using Waves.Code.Common;
using Waves.Code.Constants;
using Waves.Code.Enemies.Ranged.Resources;
using Waves.Code.Enemies.Ranged.States;
using Waves.Code.Infrastructure;
using Waves.Code.Players.Projectiles;
using Waves.Code.States;
using Waves.Code.World.Combat.CoverPoints;
using Finder = Waves.Code.World.Combat.CoverPoints.Finder;

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
		SwitchToTakingCover(Finder.Instance.Nearest(this));
	}

	public override void _PhysicsProcess(double delta)
	{
		_state.PhysicsProcess(delta);
		MoveAndSlide();
	}

	public override void _Process(double delta)
		=> _state.Process(delta);

	public void SwitchToShooting()
		=> SwitchState(new Shooting(this, _target, _shooter));

	public void SwitchToPeeking(CoverPoint current)
		=> SwitchState(new Peeking(new NavigatingRanged(this, _agent), current, _target, Profile));

	public void SwitchToTakingCover(CoverPoint coverPoint)
		=> SwitchState(new TakingCover(new NavigatingRanged(this, _agent), coverPoint, Profile));

	public void SwitchToAssaulting()
		=> SwitchState(new Assaulting(new NavigatingRanged(this, _agent), _target, Profile));

	public void SwitchToHiding(CoverPoint current)
		=> SwitchState(new Hiding(new NavigatingRanged(this, _agent), current));

	private void SwitchState(State state)
	{
		_state?.Exit();
		_state = state;
		_state.Enter();
	}

	private void OnBodyEntered(Node2D body)
	{
		body.QueueFree();
		QueueFree();
	}

	private Node2D[] HidingPoints()
		=> this.GetNodesInGroup<Node2D>(GroupNames.CoverPoint).ToArray();
}
