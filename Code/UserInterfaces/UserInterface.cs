using System;
using Godot;
using Waves.Code.Constants;
using Waves.Code.Infrastructure;
using Waves.Code.Players;

namespace Waves.Code.UserInterfaces;

public partial class UserInterface : CanvasLayer
{
    private ProgressBar _progressBar;

    public override void _Ready()
    {
        _progressBar = GetNode<ProgressBar>(UniqueNames.ProgressBar);
        SetInitialHitPoints();
        EventBus.Instance.HitPointChanged += OnHitPointChanged;
    }

    private void SetInitialHitPoints()
    {
        var player = GetTree().GetFirstNodeInGroup(GroupNames.Player) as Player ?? throw new NullReferenceException("Player not found");
        _progressBar.Value = player.CharacterProfile.HitPoints;
        _progressBar.MaxValue = player.CharacterProfile.HitPoints;
    }

    private void OnHitPointChanged(int current, int max)
    {
        _progressBar.Value = current;
        _progressBar.MaxValue = max;
    }
}