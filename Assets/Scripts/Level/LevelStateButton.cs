using UnityEngine;
using UnityEngine.Events;

public class LevelStateButton : ILevelState
{
    // for editing BNLevelData ScriptableObject
    [SerializeField] int numButtonsTarget;
    int numButtonsHit = 0;

    public UnityEvent OnLvlButtonHit = new UnityEvent();

    public override void Enter()
    {
        numButtonsHit = 0;
        OnLvlButtonHit.AddListener(() => numButtonsHit++);
    }

    public override void Update() { }

    public override void Exit() { }

    public override bool CheckWinCondition()
    {
        throw new System.NotImplementedException();
    }
}
