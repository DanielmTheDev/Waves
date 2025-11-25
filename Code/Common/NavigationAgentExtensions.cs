using Godot;

namespace Waves.Code.Common;

public static class NavigationAgentExtensions
{
    public static void UpdateAgentTarget(this NavigationAgent2D agent, Node2D target)
    {
        if (agent.TargetPosition != target.GlobalPosition)
            agent.TargetPosition = target.GlobalPosition;
    }
}