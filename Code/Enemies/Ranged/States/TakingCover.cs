using Godot;
using Waves.Code.Common;
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

        _navigatingRanged.Agent.SetVelocityToNextTarget(_navigatingRanged.Character, CoverPoint.HidingPoint.GlobalPosition, _profile.MoveSpeed);
        _navigatingRanged.Character.LookTowards(_navigatingRanged.Agent.GetNextPathPosition());
        _navigatingRanged.Character.Velocity = _navigatingRanged.Agent.Velocity;
    }

    public override void Exit()
        => _navigatingRanged.Character.Velocity = Vector2.Zero;

    private bool IsAtHidingPoint()
        => CoverPoint.HidingPoint.GlobalPosition.DistanceTo(_navigatingRanged.Character.GlobalPosition) <= 2;
}