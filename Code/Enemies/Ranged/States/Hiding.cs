using System;
using Godot;
using Waves.Code.Common.Randomness;
using Waves.Code.States;
using Waves.Code.World.Combat.CoverPoints;

namespace Waves.Code.Enemies.Ranged.States;

public class Hiding : State
{
    private readonly NavigatingRanged _enemy;
    private readonly CoverPoint _coverPoint;
    private readonly RandomTimer _randomTimer;

    public Hiding(NavigatingRanged enemy, CoverPoint coverPoint)
    {
        _enemy = enemy;
        _coverPoint = coverPoint;
        _randomTimer = new RandomTimer(1, 2);
    }

    public override void Process(double delta)
    {
        _randomTimer.ElapseTime(delta);
        if (_randomTimer.IsDone())
        {
            _enemy.Character.SwitchToPeeking(_coverPoint);
        }
    }
}

