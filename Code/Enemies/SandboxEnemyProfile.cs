using Godot;

namespace Waves.Code.Enemies;

[GlobalClass]
public sealed partial class SandboxEnemyProfile : Resource
{
    [Export] public float MoveSpeed = 120f;
    [Export] public float ShootRange = 250f;
}