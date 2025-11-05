using Godot;

namespace Waves.Code.Characters.Resources;

[GlobalClass]
public sealed partial class CharacterProfile : Resource
{
     [Export] public float MoveSpeed { get; set; }= 200f;
}