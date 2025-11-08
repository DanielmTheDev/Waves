using System;
using System.Runtime.InteropServices.ComTypes;
using Godot;
using Waves.Code.Constants;
using Waves.Code.Infrastructure;
using Waves.Code.Players;

namespace Waves.Code.UserInterfaces;

public partial class UserInterface : CanvasLayer
{
    private ProgressBar _progressBar;
    private Player _player;

    public override void _Ready()
    {
        GD.Print("initializing UI");
        EventBus.Instance.HitPointChanged += OnHitPointChanged;
        _progressBar = GetNode<ProgressBar>(UniqueNames.ProgressBar);
        _player = GetTree().GetFirstNodeInGroup(GroupNames.Player) as Player ?? throw new NullReferenceException("Player not found");
        GD.Print($"Pulled player stats {_player.CharacterProfile.HitPoints}");
        _progressBar.Value = _player.CharacterProfile.HitPoints;
        _progressBar.MaxValue = _player.CharacterProfile.HitPoints;
    }

    private void OnHitPointChanged(int current, int max)
    {
        GD.Print($"Current: {current}, Max: {max}");
        _progressBar.Value = current;
        _progressBar.MaxValue = max;
    }
}