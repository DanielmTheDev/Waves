using Godot;
using Waves.Code.Constants;
using Waves.Code.Players;

namespace Waves.Code.UserInterfaces;

public partial class UserInterface : CanvasLayer
{
    private ProgressBar _progressBar;
    private Player _player;

    public override void _Ready()
    {
        _progressBar = GetNode<ProgressBar>(UniqueNames.ProgressBar);
        _player = GetTree().GetFirstNodeInGroup(GroupNames.Player) as Player;
    }
}