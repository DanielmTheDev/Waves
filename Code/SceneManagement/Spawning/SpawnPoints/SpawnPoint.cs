using Godot;
using Waves.Code.Constants;

namespace Waves.Code.SceneManagement.Spawning.SpawnPoints;

public partial class SpawnPoint : Node2D
{
    public override void _Ready()
        => AddToGroup(GroupNames.SpawnPoint);
}