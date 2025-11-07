using Godot;

namespace Waves.Code.Players.Projectiles.Resources;

[GlobalClass]
public sealed partial class ProjectileProfile : Resource
{
    [Export] public PackedScene ProjectileScene;
    [Export] public float ProjectileSpeed = 400f;
    [Export] public float CooldownSeconds = 0.2f;
    
    [Export(PropertyHint.Layers2DPhysics)]
    public uint CollisionLayer { get; set; }

    [Export(PropertyHint.Layers2DPhysics)]
    public uint CollisionMask { get; set; }
}