namespace Waves.Code.States;

public abstract class State
{
    public virtual void Enter() {}
    public virtual void Exit() {}
    public virtual void Update(double delta) {}
    public virtual void PhysicsUpdate(double delta) {}
}