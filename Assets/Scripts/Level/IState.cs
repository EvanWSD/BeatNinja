using UnityEngine;

public abstract class IState
{
    public abstract void Enter();
    public abstract void Update();
    public abstract void Exit();
}
