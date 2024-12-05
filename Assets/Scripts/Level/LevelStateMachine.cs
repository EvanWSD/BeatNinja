using UnityEngine;
using UnityEngine.Events;

public enum SectionType
{
    None,
    Elims,
    Buttons,
    Escape
}

public class LevelStateMachine : MonoBehaviour
{
    private ILevelState state;

    public UnityEvent<ILevelState> OnLevelStateChanged = new UnityEvent<ILevelState>();
   
    public void SetState(ILevelState newState)
    {
        state?.Exit();
        state = newState;
        OnLevelStateChanged.Invoke(state);
        state.Enter();
    }

    void Update()
    {
        state?.Update();
    }

    public ILevelState GetState()
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
