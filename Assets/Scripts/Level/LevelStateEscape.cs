using System.Net.Http.Headers;
using TMPro;
using UnityEngine;

public class LevelStateEscape : ILevelState
{
    [SerializeField] float timeToEscapeMax;
    float escapeTimer;
    TextMeshProUGUI timerText;

    public LevelStateEscape(float timeToEscapeMax = 30f)
    {
        this.timeToEscapeMax = timeToEscapeMax;
    }

    public override void Enter()
    {
        escapeTimer = timeToEscapeMax;
        UI = GameObject.FindGameObjectWithTag("EscapeSectionUI");
        UI.SetActive(true);
        
        timerText = UI.GetComponentInChildren<TextMeshProUGUI>();
    }

    public override void Update() {
        escapeTimer -= Time.deltaTime;
        UpdateEscapeUI();
        if (escapeTimer <= 0)
        {
            // die/restart
        }
    }
    public override void Exit() {
        UI.SetActive(false);
    }

    public override bool CheckWinCondition() { return false; }

    void UpdateEscapeUI()
    {
         timerText.text = FormatTimer(escapeTimer);
    }

    string FormatTimer(float t)
    {
        return $"{t:00.00}";
    }


}
