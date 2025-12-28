using Godot;

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
}