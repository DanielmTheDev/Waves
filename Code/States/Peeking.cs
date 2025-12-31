using Godot;
using Waves.Code.Enemies.Ranged;
using Waves.Code.World.Combat.CoverPoints;

namespace Waves.Code.States;

public class Peeking : State
{
    private readonly NavigatingRanged _enemy;
    private readonly CoverPoint _coverPoint;
    private readonly Node2D _target;

    public Peeking(NavigatingRanged enemy, CoverPoint coverPoint, Node2D target)
    {
        _enemy = enemy;
        _coverPoint = coverPoint;
        _target = target;
    }
}
