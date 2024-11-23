using TMPro;
using UnityEditor.ShaderKeywordFilter;
using UnityEngine;
using UnityEngine.Events;

public class LevelManager : MonoBehaviour
{
    int numButtonsGoal;
    int numButtonsHit;

    [SerializeField] TextMeshProUGUI hitText;

    public void IncButtonsHit()
    {
        numButtonsHit++;
    }

    public void RefreshUI()
    {
        hitText.text = $"{numButtonsHit}";
    }
}
