using Godot;
using Waves.Code.Common;
using Waves.Code.Players.Projectiles;
using Waves.Code.States;

namespace Waves.Code.Enemies.Ranged.States;

public class Shooting : State
{
    private readonly ProjectileShooter _shooter;
    private readonly Node2D _target;
    private readonly RangedEnemy _enemy;

    public Shooting(RangedEnemy enemy, Node2D target, ProjectileShooter shooter)
    {
        _shooter = shooter;
        _target = target;
        _enemy = enemy;
    }

    public override void PhysicsProcess(double delta)
    {
        var distanceToTarget = _target.GlobalPosition.DistanceTo(_enemy.GlobalPosition);
        if (distanceToTarget > _enemy.Profile.ShootRange)
        {
            _enemy.SwitchToTakingCover();
            return;
        }
        _shooter.TryShootAt(_target.GlobalPosition);
    }
}