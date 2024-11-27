using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LevelStateElim : ILevelState
{
    int numElims = 0;
    int numElimsTarget;

    public UnityEvent OnEnemyElim = new UnityEvent();

    public override void Enter()
    {
        numElims = 0;
        OnEnemyElim.AddListener(() =>
        {
            numElims++;
            CheckWinCondition();
        });
    }

    public override void Update() { }
    public override void Exit() { }

    public override bool CheckWinCondition()
    {
        return numElims == numElimsTarget;
    }


}
