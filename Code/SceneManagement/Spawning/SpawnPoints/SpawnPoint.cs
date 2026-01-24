using Godot;
using Waves.Code.Constants;

namespace Waves.Code.SceneManagement.Spawning.SpawnPoints;

public partial class SpawnPoint : Node2D
{
    [Export]
    public Area2D ActivationArea { get; set; }
    public bool IsActive { get; private set; }

    public override void _Ready()
    {
        AddToGroup(GroupNames.SpawnPoint);
        InitializeActivationArea();
    }

    private void InitializeActivationArea()
    {
        ActivationArea.BodyEntered += body =>
        {
            if (body.IsInGroup(GroupNames.Player))
                IsActive = true;
        };

        ActivationArea.BodyExited += body =>
        {
            if (body.IsInGroup(GroupNames.Player))
                IsActive = false;
        };
    }
}