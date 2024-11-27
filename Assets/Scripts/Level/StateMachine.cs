using System;
using Unity.VisualScripting;
using UnityEngine;

public enum SectionType
{
    None,
    Elims,
    Buttons,
    Escape
}

public class StateMachine : MonoBehaviour
{
    private IState state;
   
    public void SetState(IState newState)
    {
        state?.Exit();
        state = newState;
        state.Enter();
    }

    void Update()
    {
        state?.Update();
    }

    public IState GetState()
    {
        return state;
    }

    public void SetLevelStateFromEnum(SectionType sectionType)
    {
        switch (sectionType)
        {
            case SectionType.None: SetState(new LevelStateNone()); break;
            case SectionType.Elims: SetState(new LevelStateElim()); break;
            case SectionType.Buttons: SetState(new LevelStateButton()); break;
            case SectionType.Escape: SetState(new LevelStateEscape()); break;
            default: SetState(new LevelStateNone()); break;
        }
    }
}
