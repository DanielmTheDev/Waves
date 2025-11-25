using Godot;
using Waves.Code.Players.Projectiles;
using Waves.Code.States;

namespace Waves.Code.Enemies.Ranged.States;

public class Shooting : State
{
    private readonly ProjectileShooter _shooter;
    private readonly Node2D _target;
    private readonly RangedEnemy _character;

    public Shooting(RangedEnemy character, Node2D target, ProjectileShooter shooter)
    {
        _shooter = shooter;
        _target = target;
        _character = character;
    }

    public override void PhysicsUpdate(double delta)
    {
        var distanceToTarget = _target.GlobalPosition.DistanceTo(_character.GlobalPosition);
        if (distanceToTarget > _character.Profile.ShootRange)
        {
            _character.SwitchToFollowing();
            return;
        }
        _shooter.TryShootAt(_target.GlobalPosition);
    }
}