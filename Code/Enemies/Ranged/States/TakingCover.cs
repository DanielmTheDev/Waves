using Godot;
using Waves.Code.Enemies.Ranged.Resources;
using Waves.Code.States;
using Waves.Code.World.Combat.CoverPoints;

namespace Waves.Code.Enemies.Ranged.States;

public class TakingCover : State
{
    private readonly NavigatingRanged _enemy;
    private readonly CoverPoint _coverPoint;
    private readonly RangedEnemyProfile _profile;

    public TakingCover(NavigatingRanged enemy, CoverPoint _coverPoint, RangedEnemyProfile profile)
    {
        _enemy = enemy;
        this._coverPoint = _coverPoint;
        _profile = profile;
    }

    public override void PhysicsProcess(double delta)
    {
        if (IsAtHidingPoint())
        {
            _enemy.Character.SwitchToHiding(_coverPoint);
            return;
        }
        _enemy.MoveTowards(_coverPoint.HidingPoint.GlobalPosition, _profile.MoveSpeed);
    }

    public override void Exit()
        => _enemy.Stop();

    private bool IsAtHidingPoint()
        => _enemy.IsAtPosition(_coverPoint.HidingPoint.GlobalPosition);
}