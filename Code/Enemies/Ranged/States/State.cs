namespace Waves.Code.Enemies.Ranged.States;

public abstract class State
{
    public abstract void Enter();
    public abstract void Exit();
    public abstract void Update(double delta);
}