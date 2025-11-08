using Godot;

namespace Waves.Code.Infrastructure;

public sealed partial class EventBus : Node
{
    [Signal]
    public delegate void HitPointChangedEventHandler(int current, int max);

    public static EventBus Instance { get; private set; }

    public override void _EnterTree()
        => Instance = this;

    public void EmitHitPointChanged(int current, int max)
        => EmitSignalHitPointChanged(current, max);
}