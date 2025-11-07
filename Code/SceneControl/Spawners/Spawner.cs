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
    private RandomNumberGenerator _rng = new();

    public override void _Ready()
    {
        _timer = GetNode<Timer>(UniqueNames.Timer);
        _timer.WaitTime = Profile.SpawnCycle;
        _timer.Timeout += SpawnEnemies;
        _timer.Start();
    }

    private void SpawnEnemies()
    {
        var spawnPoints = SpawnPoints();
        var numberOfEnemiesToSpawn = NrEnemiesToSpawn(spawnPoints);
        SpawnEnemies(numberOfEnemiesToSpawn, spawnPoints);
    }

    private Stack<SpawnPoint> SpawnPoints()
        => new(GetTree()
            .GetNodesInGroup(GroupNames.SpawnPoint)
            .OfType<SpawnPoint>()
            .OrderBy(_ => _rng.Randi()));

    private int NrEnemiesToSpawn(Stack<SpawnPoint> spawnPoints)
    {
        var maxNumberOfEnemiesToSpawn = Profile.EnemyCountPerCycle > spawnPoints.Count
            ? spawnPoints.Count
            : Profile.EnemyCountPerCycle;
        return int.Min(maxNumberOfEnemiesToSpawn, DifferenceToTotalEnemies());
    }

    private void SpawnEnemies(int numberOfEnemiesToSpawn, Stack<SpawnPoint> spawnPoints)
    {
        for (var i = 0; i < numberOfEnemiesToSpawn; i++)
        {
            var spawnPoint = spawnPoints.Pop();
            var enemy = EnemyScene.Instantiate<SandboxEnemy>();
            GetTree().CurrentScene.AddChild(enemy);
            enemy.GlobalPosition = spawnPoint.GlobalPosition;
        }
    }

    private int DifferenceToTotalEnemies()
        => Profile.MaxTotalEnemies - GetTree().GetNodeCountInGroup(GroupNames.Enemy);
}