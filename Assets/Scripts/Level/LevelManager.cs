using TMPro;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    int numButtonsGoal;
    int numButtonsHit;

    public StateMachine sm;

    [SerializeField] TextMeshProUGUI hitText;

    private void Start()
    {
        sm = GetComponent<StateMachine>();
        sm.SetState(new LevelStateNone());
    }

    public void IncButtonsHit()
    {
        numButtonsHit++;
    }

    public void RefreshUI()
    {
        hitText.text = $"{numButtonsHit}";
    }
}
