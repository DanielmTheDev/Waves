using Godot;

namespace Waves.Code.Enemies.Melee;

public partial class MeleeEnemy : CharacterBody2D
{
    [Export] public Resources.MeleeEnemyProfile Profile { get; set; }
}