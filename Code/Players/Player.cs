using Godot;
using Waves.Code.Common;
using Waves.Code.Constants;
using Waves.Code.Infrastructure;
using Waves.Code.Players.Projectiles;
using Waves.Code.Players.Resources;

namespace Waves.Code.Players;

public partial class Player : CharacterBody2D
{
    [Export] public CharacterProfile CharacterProfile;
    private ProjectileShooter _projectileShooter;
    private Area2D _area2D;
    private HitPoints _hitPoints;
    private Vector2 _lastDirection;
    private AnimationPlayer _animationPlayer;

    public override void _Ready()
    {
        AddToGroup(GroupNames.Player);
        _hitPoints = new(CharacterProfile.HitPoints, CharacterProfile.HitPoints);
        _projectileShooter = GetNode<ProjectileShooter>(UniqueNames.ProjectileShooter);
        _animationPlayer = GetNode<AnimationPlayer>(UniqueNames.AnimationPlayer);
        _area2D = GetNode<Area2D>(UniqueNames.Area2d);
        _area2D.BodyEntered += OnBodyEntered;
        _area2D.AreaEntered += OnBodyEntered;
    }

    public override void _PhysicsProcess(double delta)
    {
        ProcessMovement();
        ProcessAnimation();
        ProcessAttack();
    }

    private void ProcessAttack()
    {
        if (!Input.IsActionJustPressed("shoot"))
            return;
        _projectileShooter.TryShootAt(GetGlobalMousePosition());
    }

    private void ProcessMovement()
    {
        Velocity = ReadInput().Velocity(CharacterProfile.MoveSpeed);
        MoveAndSlide();
    }

    private void OnBodyEntered(Node2D body)
    {
        _hitPoints = _hitPoints with { Current = _hitPoints.Current - 1 };
        EventBus.Instance.EmitHitPointChanged(_hitPoints.Current, _hitPoints.Max);
        body.QueueFree();
    }

    private void ProcessAnimation()
    {
        var direction = ReadInput();
        if (direction != Vector2.Zero)
        {
            _lastDirection = direction;
            PlayIdleAnimation(_lastDirection);
        }
    }

    private void PlayIdleAnimation(Vector2 direction)
    {
        var animationName = GetAnimationName("idle", direction);
        if (_animationPlayer.CurrentAnimation != animationName)
        {
            _animationPlayer.Play(animationName);
        }
    }

    private static string GetAnimationName(string type, Vector2 direction)
    {
        var angle = direction.Angle();
        var octant = Mathf.FloorToInt((angle + Mathf.Pi / 8f) / (Mathf.Pi / 4f)) & 7;

        return octant switch
        {
            0 => $"{type}_right",         // →
            1 => $"{type}_front_right",   // ↘
            2 => $"{type}_front",         // ↓
            3 => $"{type}_front_left",    // ↙
            4 => $"{type}_left",          // ←
            5 => $"{type}_back_left",     // ↖
            6 => $"{type}_back",          // ↑
            7 => $"{type}_back_right",    // ↗
            _ => $"{type}_front"
        };
    }

    private static Vector2 ReadInput()
        => Input.GetVector("move_left", "move_right", "move_up", "move_down");
}