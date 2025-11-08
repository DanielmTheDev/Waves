using Godot;

namespace Waves.Code.Enemies.Ranged;

[GlobalClass]
public sealed partial class RangedEnemyProfile : Resource
{
    [Export] public float MoveSpeed = 120f;
    [Export] public float ShootRange = 250f;
}