using Godot;
using Waves.Code.Constants;

namespace Waves.Code.Cameras;

public partial class PlayerCamera : Camera2D
{
    private Node2D Player;

    public override void _Ready()
    {
        MakeCurrent();
        PositionSmoothingEnabled = true;
        PositionSmoothingSpeed = Mathf.Max(0.01f, 7f);
        Player = GetTree().GetFirstNodeInGroup(GroupNames.Player) as Node2D;
    }

    public override void _Process(double delta)
        => ApplyFollow();

    private void ApplyFollow ()
        => GlobalPosition = Player.GlobalPosition;
}