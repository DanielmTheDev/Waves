using Godot;

namespace Waves.Code.Common.Randomness;

public class RandomTimer
{
    private float _waitTime;

    public RandomTimer(float min, float max)
        => _waitTime = (float)GD.RandRange(min, max);

    public void ElapseTime(double delta)
        => _waitTime -= (float)delta;

    public bool IsDone()
        => _waitTime <= 0;
}