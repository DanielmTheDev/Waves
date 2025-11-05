using Godot;

namespace Waves.Code.Characters.Core;

public static class AimLogic
{
    public static float ComputeLookRotation(Vector2 originPos, Vector2 targetPos)
        => (targetPos - originPos).Angle();
}