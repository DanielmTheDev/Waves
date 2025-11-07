using Godot;
using Waves.Code.Players.Core;
using Waves.Code.Players.Projectiles.Resources;

namespace Waves.Code.Players.Projectiles;

public partial class ProjectileShooter : Node2D
{
    [Export] public ProjectileProfile Profile;

    private Cooldown _cooldown;

    public override void _Ready()
        => _cooldown = new(Profile.CooldownSeconds);

    public void TryShootAt(Vector2 targetPosition)
    {

        var now = Time.GetTicksMsec() / 1000f;
        if (!_cooldown.CanShoot(now))
            return;

        Shoot(targetPosition);
        _cooldown.RecordShot(now);
    }

    private void Shoot(Vector2 targetPosition)
    {
        var proj = Profile.ProjectileScene.Instantiate<RigidBody2D>();
        proj.CollisionLayer = Profile.CollisionLayer;
        proj.CollisionMask  = Profile.CollisionMask;
        GetTree().CurrentScene.AddChild(proj);
        var dir = (targetPosition - GlobalPosition).Normalized();
        proj.GlobalPosition = GlobalPosition;
        proj.LinearVelocity = dir * Profile.ProjectileSpeed;
    }
}