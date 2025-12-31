using Godot;
using Waves.Code.Enemies.Ranged.Resources;
using Waves.Code.States;
using Waves.Code.World.Combat.CoverPoints;

namespace Waves.Code.Enemies.Ranged.States;

public class TakingCover : State
{
    private readonly CoverPoint CoverPoint;
    private readonly NavigatingRanged _enemy;
    private readonly RangedEnemyProfile _profile;

    public TakingCover(NavigatingRanged enemy, CoverPoint[] coverPoints, RangedEnemyProfile profile)
    {
        _enemy = enemy;
        _profile = profile;
        CoverPoint = coverPoints.NearestTo(enemy.Character);
    }

    public override void PhysicsProcess(double delta)
    {
        if (IsAtHidingPoint())
        {
            _enemy.Character.SwitchToHiding(CoverPoint);
            return;
        }
        _enemy.MoveTowards(CoverPoint.HidingPoint.GlobalPosition, _profile.MoveSpeed);
    }

    public override void Exit()
        => _enemy.Stop();

    private bool IsAtHidingPoint()
        => _enemy.IsAtPosition(CoverPoint.HidingPoint.GlobalPosition);
}