using Godot;
using Waves.Code.Characters.Core;
using Waves.Code.Characters.Projectiles.Resources;

namespace Waves.Code.Characters.Projectiles;

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
        GetTree().CurrentScene.AddChild(proj);
        var dir = (targetPosition - GlobalPosition).Normalized();
        proj.GlobalPosition = GlobalPosition;
        proj.LinearVelocity = dir * Profile.ProjectileSpeed;
    }
}