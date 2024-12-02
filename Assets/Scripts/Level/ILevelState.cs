using System;
using TMPro.EditorUtilities;
using UnityEngine;

public abstract class ILevelState : IState
{
    protected GameObject UI;
    public virtual bool CheckWinCondition() { return false; }
}
