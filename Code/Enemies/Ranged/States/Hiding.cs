using System;
using Godot;
using Waves.Code.States;
using Waves.Code.World.Combat.CoverPoints;

namespace Waves.Code.Enemies.Ranged.States;

public class Hiding : State
{
    private readonly NavigatingRanged _enemy;
    private readonly CoverPoint _coverPoint;
    private float _waitTime;

    public Hiding(NavigatingRanged enemy, CoverPoint coverPoint)
    {
        _enemy = enemy;
        _coverPoint = coverPoint;
        _waitTime = Random.Shared.NextSingle() * 2 + 1;
    }

    public override void Process(double delta)
    {
        GD.Print("Hiding Update");
        base.Process(delta);
        _waitTime -= (float)delta;
        if (_waitTime <= 0)
        {
            _enemy.Character.SwitchToPeeking(_coverPoint);
        }
    }
}
