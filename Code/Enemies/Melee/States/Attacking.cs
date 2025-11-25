using Godot;
using Waves.Code.States;

namespace Waves.Code.Enemies.Melee.States;

public class Attacking : State
{
    private readonly Node2D _target;
    private readonly MeleeEnemy _character;

    public Attacking(MeleeEnemy character, Node2D target)
    {
        _target = target;
        _character = character;
    }


    public override void PhysicsUpdate(double delta)
    {
        var distanceToTarget = _target.GlobalPosition.DistanceTo(_character.GlobalPosition);
        if (distanceToTarget > _character.Profile.Range)
        {
            _character.SwitchToFollowing();
        }
    }
}