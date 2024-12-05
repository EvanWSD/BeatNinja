using UnityEngine;

public class SectionUI : MonoBehaviour
{
    LevelManager lvl;

    [SerializeField] public GameObject elimUI;
    [SerializeField] public GameObject btnUI;
    [SerializeField] public GameObject escapeUI;
    [SerializeField] public GameObject noneUI;

    GameObject activeUI;

    private void Start()
    {
        activeUI = noneUI;
        lvl = GetComponent<LevelManager>();
        lvl.sm.OnLevelStateChanged.AddListener((ILevelState newState) =>
        {
            activeUI.SetActive(false);
            switch(newState)
            {
                case LevelStateElim: activeUI = elimUI; break;
                case LevelStateButton: activeUI = btnUI; break;
                case LevelStateEscape: activeUI = escapeUI; break;
                default: activeUI = noneUI; break;
            }
            activeUI.SetActive(true);
        });
    }
}
