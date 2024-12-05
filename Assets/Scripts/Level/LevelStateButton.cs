using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class LevelStateButton : ILevelState
{
    [SerializeField] int numButtonsTarget;
    int numButtonsHit = 0;

    TextMeshProUGUI buttonsHitText;

    public UnityEvent OnLvlButtonHit = new UnityEvent();

    public LevelStateButton(int numButtonsTarget = 5)
    {
        this.numButtonsTarget = numButtonsTarget;
    }

    public override void Enter()
    {
        numButtonsHit = 0;
        OnLvlButtonHit.AddListener(() => {
            numButtonsHit++;
            UpdateButtonsHitUI();
        });
    }

    public override void Update() { }

    public override void Exit() { }

    public override bool CheckWinCondition()
    {
        return numButtonsHit >= numButtonsTarget;
    }

    void UpdateButtonsHitUI()
    {
        buttonsHitText.text = $"{numButtonsHit} / {numButtonsTarget}";
    }
}
