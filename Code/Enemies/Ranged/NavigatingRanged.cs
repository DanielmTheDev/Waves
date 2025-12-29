using Godot;
using Waves.Code.Common;

namespace Waves.Code.Enemies.Ranged;

public class NavigatingRanged
{
    public readonly RangedEnemy Character;
    public readonly NavigationAgent2D Agent;

    public NavigatingRanged(RangedEnemy character, NavigationAgent2D agent)
    {
        Character = character;
        Agent = agent;
    }

    public void MoveTowards(Vector2 target, float speed)
    {
        Agent.SetVelocityToNextTarget(Character, target, speed);
        Character.LookTowards(Agent.GetNextPathPosition());
        Character.Velocity = Agent.Velocity;
    }

    public void Stop()
        => Character.Velocity = Vector2.Zero;

    public bool IsAtPosition(Vector2 hidingPointGlobalPosition, float tolerance = 2)
        => hidingPointGlobalPosition.DistanceTo(Character.GlobalPosition) <= tolerance;
}