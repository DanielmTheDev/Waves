using Godot;

namespace Waves.Code.Infrastructure;

public sealed partial class EventBus : Node
{
    [Signal]
    public delegate void HealthChangedEventHandler(int current, int max);

    public static EventBus Instance { get; private set; }

    public override void _Ready()
        => Instance = this;
}