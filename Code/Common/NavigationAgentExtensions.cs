using Godot;

namespace Waves.Code.Common;

public static class NavigationAgentExtensions
{
    public static void SetVelocityToNextTarget(this NavigationAgent2D agent, Node2D character, Vector2 targetPosition, float moveSpeed)
    {
        agent.SetTargetPosition(targetPosition);
        if (agent.IsNavigationFinished())
        {
            agent.SetVelocity(Vector2.Zero);
            return;
        }

        var desired = character.GlobalPosition.NormalizedTo(agent.GetNextPathPosition()) * moveSpeed;
        agent.SetVelocity(desired);
    }
}