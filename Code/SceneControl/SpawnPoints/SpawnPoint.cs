using Godot;
using Waves.Code.Constants;

namespace Waves.Code.SceneControl.SpawnPoints;

public partial class SpawnPoint : Node2D
{
    public override void _Ready()
        => AddToGroup(GroupNames.SpawnPoint);
}