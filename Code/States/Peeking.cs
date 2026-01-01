using Godot;
using Waves.Code.Common;
using Waves.Code.Enemies.Ranged;
using Waves.Code.Enemies.Ranged.Resources;
using Waves.Code.World.Combat.CoverPoints;

namespace Waves.Code.States;

public class Peeking : State
{
    private readonly NavigatingRanged _navigator;
    private readonly CoverPoint _coverPoint;
    private readonly Node2D _target;
    private readonly RangedEnemyProfile _profile;

    public Peeking(NavigatingRanged enemy, CoverPoint coverPoint, Node2D target, RangedEnemyProfile profile)
    {
        _navigator = enemy;
        _coverPoint = coverPoint;
        _target = target;
        _profile = profile;
    }

    public override void PhysicsProcess(double delta)
    {
        if (_navigator.Character.CanClearPath(_target, 10f, PhysicsLayers.World))
        {
            _navigator.Stop();
            _navigator.Character.SwitchToShooting();
            return;
        }
        _navigator.MoveTowards(_coverPoint.ShootingPoint.GlobalPosition, _profile.MoveSpeed);

        // todo: this cant stay that way
        if (_navigator.IsAtPosition(_coverPoint.ShootingPoint.GlobalPosition))
        {
            _navigator.Stop();
            _navigator.Character.SwitchToShooting();
        }
    }
}
