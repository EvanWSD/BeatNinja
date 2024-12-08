using TMPro;
using UnityEngine;

public class LevelStateEscape : ILevelState
{
    [SerializeField] float timeToEscapeMax;
    float escapeTimer;
    TextMeshProUGUI timerText;

    GameObject player;

    public LevelStateEscape(float timeToEscapeMax = 60f)
    {
        this.timeToEscapeMax = timeToEscapeMax;
    }

    public override void Enter()
    {
        player = GameObject.FindGameObjectWithTag("Player");
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
            escapeTimer = timeToEscapeMax;
            player.GetComponent<PlrDeath>().OnPlayerDeath.Invoke();
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
