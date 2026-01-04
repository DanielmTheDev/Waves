using Godot;
using Waves.Code.Common.Randomness;
using Waves.Code.Players.Projectiles;
using Waves.Code.States;
using Waves.Code.World.Combat.CoverPoints;

namespace Waves.Code.Enemies.Ranged.States;

public class Shooting : State
{
    private readonly ProjectileShooter _shooter;
    private readonly Node2D _target;
    private readonly RangedEnemy _enemy;
    private readonly RandomTimer _randomTimer;

    public Shooting(RangedEnemy enemy, Node2D target, ProjectileShooter shooter)
    {
        _shooter = shooter;
        _target = target;
        _enemy = enemy;
        _randomTimer = new RandomTimer(3, 5);
    }

    public override void PhysicsProcess(double delta)
    {
        _randomTimer.ElapseTime(delta);
        var distanceToTarget = _target.GlobalPosition.DistanceTo(_enemy.GlobalPosition);
        if (distanceToTarget > _enemy.Profile.ShootRange || _randomTimer.IsDone())
        {
            Finder.Instance.FreeNearestToPlayer().Match(
                point => _enemy.SwitchToTakingCover(point),
                () => _enemy.SwitchToAssaulting());
            return;
        }
        _shooter.TryShootAt(_target.GlobalPosition);
    }
}