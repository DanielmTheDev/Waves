namespace Waves.Code.Enemies.Ranged.States;

public abstract class State
{
    public abstract void Enter();
    public abstract void Exit();
    public virtual void Update(double delta) {}
    public virtual void PhysicsUpdate(double delta) {}
}