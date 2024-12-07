using TMPro;
using UnityEditor.PackageManager.UI;
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
            UpdateUI();
        });

        UI = GameObject.FindGameObjectWithTag("ButtonSectionUI");
        UI.SetActive(true);
        buttonsHitText = UI.GetComponentInChildren<TextMeshProUGUI>();
        UpdateUI();
    }

    public override void Update() { }

    public override void Exit() { }

    public override bool CheckWinCondition()
    {
        return numButtonsHit >= numButtonsTarget;
    }

    void UpdateUI()
    {
        buttonsHitText.text = $"Buttons: {numButtonsHit} / {numButtonsTarget}";
    }
}
