using System.Collections.Generic;
using System.Linq;
using Godot;
using Waves.Code.Constants;
using Waves.Code.Enemies;
using Waves.Code.SceneControl.Resources;
using Waves.Code.SceneControl.SpawnPoints;

namespace Waves.Code.SceneControl.Spawners;

public partial class Spawner : Node
{
    [Export] private SpawnerProfile Profile { get; set; }
    [Export] private PackedScene EnemyScene { get; set; }
    private Timer _timer;
    private RandomNumberGenerator rng = new();

    public override void _Ready()
    {
        _timer = GetNode<Timer>(UniqueNames.Timer);
        _timer.WaitTime = Profile.SpawnCycle;
        _timer.Timeout += SpawnEnemies;
        _timer.Start();
    }

    private void SpawnEnemies()
    {
        var spawnPoints = new Stack<SpawnPoint>(GetTree()
            .GetNodesInGroup(GroupNames.SpawnPoint)
            .OfType<SpawnPoint>()
            .OrderBy(_ => rng.Randi()));

        var numberOfEnemiesToSpawn = Profile.EnemyCountPerCycle > spawnPoints.Count
            ? spawnPoints.Count
            : Profile.EnemyCountPerCycle;
        for (var i = 0; i < numberOfEnemiesToSpawn; i++)
        {
            var spawnPoint = spawnPoints.Pop();
            var enemy = EnemyScene.Instantiate<SandboxEnemy>();
            GetTree().CurrentScene.AddChild(enemy);
            enemy.GlobalPosition = spawnPoint.GlobalPosition;
        }
    }
}