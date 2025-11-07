using Godot;
using Waves.Code.Enemies;
using Waves.Code.SceneControl.Resources;

namespace Waves.Code.SceneControl.Spawners;

public partial class Spawner : Node
{
    [Export] private SpawnerProfile Profile { get; set; }
    [Export] private PackedScene EnemyScene { get; set; }
    private Timer _timer;

    public override void _Ready()
    {
        _timer = GetNode<Timer>(UniqueNames.Timer);
        _timer.WaitTime = Profile.SpawnCycle;
        _timer.Timeout += SpawnEnemies;
        _timer.Start();
    }

    private void SpawnEnemies()
    {
        for (var i = 0; i < Profile.EnemyCountPerCycle; i++)
        {
            GD.Print("Spawning enemy");
            var enemy = EnemyScene.Instantiate<SandboxEnemy>();
            GetTree().CurrentScene.AddChild(enemy);
            enemy.GlobalPosition = new(500, i*100);
        }
    }
}