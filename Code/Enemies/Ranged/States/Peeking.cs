using System;
using Godot;
using LanguageExt;
using Waves.Code.Common;
using Waves.Code.Enemies.Ranged.Resources;
using Waves.Code.States;
using Waves.Code.World.Combat.CoverPoints;

namespace Waves.Code.Enemies.Ranged.States;

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

        if (_navigator.IsAtPosition(_coverPoint.ShootingPoint.GlobalPosition))
            SwitchToNextState();
    }

    private void SwitchToNextState()
    {
        switch (GetNextAction())
        {
            case NextAction.TakeCover:
                _navigator.Character.SwitchToTakingCover(_coverPoint);
                break;
            case NextAction.Reposition:
                Finder.Instance.RandomFree().Match(
                    point => _navigator.Character.SwitchToTakingCover(point),
                    () => _navigator.Character.SwitchToAssaulting());
                break;
            case NextAction.Attack:
                _navigator.Character.SwitchToAssaulting();
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private static NextAction GetNextAction()
        => GD.Randf() switch
        {
            > 0.7f => NextAction.Attack,
            > 0.4f => NextAction.Reposition,
            _ => NextAction.TakeCover
        };

    private enum NextAction
    {
        TakeCover,
        Reposition,
        Attack
    }
}