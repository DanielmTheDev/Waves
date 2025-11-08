using Godot;

namespace Waves.Code.SceneManagement.Spawning.Resources;

[GlobalClass]
public sealed partial class SpawnerProfile : Resource
{
     [Export] public float SpawnCycle { get; set; } = 2f;
     [Export] public int EnemyCountPerCycle { get; set; } = 3;
     [Export] public int MaxTotalEnemies { get; set; } = 5;
}