using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class LevelStateButton : ILevelState
{
    // for editing BNLevelData ScriptableObject
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

        UI = GameObject.FindGameObjectWithTag("ButtonSectionUI");
        UI.SetActive(true);
        buttonsHitText = UI.GetComponentInChildren<TextMeshProUGUI>();
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
