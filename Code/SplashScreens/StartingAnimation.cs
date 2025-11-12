using Godot;

namespace Waves.Code.SplashScreens;

public partial class StartingAnimation : Control
{
    [Export] private PackedScene _nextScene;

    public override void _Ready()
    {
        var videoStreamPlayer = GetNode<VideoStreamPlayer>(UniqueNames.VideoStreamPlayer);
        videoStreamPlayer.Finished += OnVideoStreamPlayerFinished;
    }

    private void OnVideoStreamPlayerFinished()
        => GetTree().ChangeSceneToPacked(_nextScene);
}