using Godot;

namespace Waves.Code.Characters.Projectiles.Resources;

[GlobalClass]
public sealed partial class ProjectileProfile : Resource
{
    [Export] public PackedScene ProjectileScene;
    [Export] public float ProjectileSpeed = 400f;
    [Export] public float CooldownSeconds = 0.2f;
}