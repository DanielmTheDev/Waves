using Godot;
using Waves.Code.Common;
using Waves.Code.Enemies.Ranged.Resources;
using Waves.Code.States;

namespace Waves.Code.Enemies.Ranged.States;

public class Assaulting : State
{
    private readonly NavigatingRanged _navigator;
    private readonly Node2D _target;
    private readonly RangedEnemyProfile _profile;

    public Assaulting(NavigatingRanged enemy, Node2D target, RangedEnemyProfile profile)
    {
        _navigator = enemy;
        _target = target;
        _profile = profile;
    }

    public override void PhysicsProcess(double delta)
    {
        if (_navigator.Character.CanClearPath(_target, 10f, PhysicsLayers.World)
            && _navigator.Character.GlobalPosition.DistanceTo(_target.GlobalPosition) < _profile.ShootRange)
        {
            _navigator.Stop();
            _navigator.Character.SwitchToShooting();
            return;
        }
        _navigator.MoveTowards(_target.GlobalPosition, _profile.MoveSpeed);
    }
}