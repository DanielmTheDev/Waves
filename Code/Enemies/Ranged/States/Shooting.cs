using Godot;
using Waves.Code.Players.Projectiles;

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

    public override void Enter()
    {
    }

    public override void Exit()
    {
    }

    public override void Update(double delta)
    {
        var toTarget = _target.GlobalPosition - _character.GlobalPosition;
        if (toTarget.Length() > _character.Profile.ShootRange)
        {
            _character.SwitchToFollowing();
            return;
        }
        _shooter.TryShootAt(_target.GlobalPosition);
    }
}