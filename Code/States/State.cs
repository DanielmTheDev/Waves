namespace Waves.Code.States;

public abstract class State
{
    public virtual void Enter() {}
    public virtual void Exit() {}
    public virtual void Process(double delta) {}
    public virtual void PhysicsProcess(double delta) {}
}