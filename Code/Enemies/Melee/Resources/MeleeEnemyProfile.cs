using Godot;

namespace Waves.Code.Enemies.Melee.Resources;

[GlobalClass]
public sealed partial class MeleeEnemyProfile : Resource
{
    [Export] public float MoveSpeed = 120f;
    [Export] public float Range = 10f;
}