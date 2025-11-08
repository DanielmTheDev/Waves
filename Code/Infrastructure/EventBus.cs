using Godot;

namespace Waves.Code.Infrastructure;

public sealed partial class EventBus : Node
{
    [Signal]
    public delegate void HitPointChangedEventHandler(int current, int max);

    public static EventBus Instance { get; private set; }

    public override void _EnterTree()
    {
        GD.Print("Initializing Eventbus");
        Instance = this;
    }

    public void EmitHitPointChanged(int current, int max)
    {
        GD.Print("Emitting health changed in bus");
        EmitSignalHitPointChanged(current, max);
    }
}