using Godot;
using Waves.Code.Characters.Core;

namespace Waves.Code.Characters.Projectiles;

public partial class ProjectileShooter : Node2D
{
    [Export] public Resources.ProjectileProfile Profile;
    private Cooldown _cooldown;

    public override void _Ready()
        => _cooldown = new(Profile.CooldownSeconds);

    public override void _PhysicsProcess(double delta)
    {
        if (!Input.IsActionJustPressed("shoot"))
            return;

        var now = (float)EngineTimeInSeconds();
        if (!_cooldown.CanShoot(now))
            return;

        ShootTowardsMouse();
        _cooldown.RecordShot(now);
    }

    private void ShootTowardsMouse()
    {
        var projectile = Profile.ProjectileScene.Instantiate<RigidBody2D>();
        var world = GetTree().CurrentScene;
        world.AddChild(projectile);
        var origin = GlobalPosition;
        var mouse = GetGlobalMousePosition();
        var dir = (mouse - origin).Normalized();
        projectile.GlobalPosition = origin;
        projectile.LinearVelocity = dir * Profile.ProjectileSpeed;
    }

    private static double EngineTimeInSeconds()
        => Time.GetTicksMsec() / 1000.0;
}