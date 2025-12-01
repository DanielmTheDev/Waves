using Godot;

namespace Waves.Code.Common;

public static class NavigationAgentExtensions
{
    public static void SetVelocityToNextTarget(this NavigationAgent2D agent, Vector2 targetPosition, float moveSpeed)
    {
        agent.SetTargetPosition(targetPosition);
        if (agent.IsNavigationFinished())
        {
            agent.SetVelocity(Vector2.Zero);
            return;
        }

        var desired = agent.GetNextPathPosition().DirectionTo(targetPosition) * moveSpeed;
        agent.SetVelocity(desired);
    }
}