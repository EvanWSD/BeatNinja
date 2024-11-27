using System;

public abstract class ILevelState : IState
{
    public virtual bool CheckWinCondition() { return false; }
}
