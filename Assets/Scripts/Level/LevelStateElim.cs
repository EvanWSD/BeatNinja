using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class LevelStateElim : ILevelState
{
    int numElims = 0;
    int numElimsTarget;
    TextMeshProUGUI elimCounterText;

    PlrDeath player;

    public UnityEvent OnEnemyElim = new UnityEvent();

    public LevelStateElim(int numElimsTarget = 10) {
        this.numElimsTarget = numElimsTarget;
    }

    public override void Enter()
    {
        numElims = 0;
        OnEnemyElim.AddListener(() =>
        {
            numElims++;
            ResetUI();
        });

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlrDeath>();
        player.OnPlayerDeath.AddListener(() => { 
            numElims = 0;
            ResetUI();
        });


        UI = GameObject.FindGameObjectWithTag("ElimSectionUI");
        UI.SetActive(true);
        elimCounterText = UI.GetComponentInChildren<TextMeshProUGUI>();
        ResetUI();
    }

    public override void Update() { }
    public override void Exit() {
        UI.SetActive(false);
    }

    public override bool CheckWinCondition()
    {
        return numElims >= numElimsTarget;
    }

    void ResetUI()
    {
        elimCounterText.text = $"Elim: {numElims} / {numElimsTarget}";
    }


}
