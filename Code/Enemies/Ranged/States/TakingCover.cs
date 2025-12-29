using Waves.Code.Enemies.Ranged.Resources;
using Waves.Code.States;
using Waves.Code.World.Combat.CoverPoints;

namespace Waves.Code.Enemies.Ranged.States;

public class TakingCover : State
{
    private readonly CoverPoint CoverPoint;
    private readonly NavigatingRanged _navigatingRanged;
    private readonly RangedEnemyProfile _profile;

    public TakingCover(NavigatingRanged navigatingRanged, CoverPoint[] coverPoints, RangedEnemyProfile profile)
    {
        _navigatingRanged = navigatingRanged;
        _profile = profile;
        CoverPoint = coverPoints.NearestTo(navigatingRanged.Character);
    }

    public override void PhysicsUpdate(double delta)
    {
        if (IsAtHidingPoint())
        {
            _navigatingRanged.Character.SwitchToHiding(CoverPoint);
            return;
        }
        _navigatingRanged.MoveTowards(CoverPoint.HidingPoint.GlobalPosition, _profile.MoveSpeed);
    }

    public override void Exit()
        => _navigatingRanged.Stop();

    private bool IsAtHidingPoint()
        => _navigatingRanged.IsAtPosition(CoverPoint.HidingPoint.GlobalPosition);
}