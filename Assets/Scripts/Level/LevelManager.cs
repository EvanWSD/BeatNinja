using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public LevelStateMachine sm;
    public GameObject currSectionEndDoor;

    void Start()
    {
        sm = GetComponent<LevelStateMachine>();
        sm.SetState(new LevelStateNone());
    }

    void Update()
    {
        // on section goal complete
        if (sm.GetState().CheckWinCondition())
        {
            currSectionEndDoor.SetActive(false);
            sm.SetState(new LevelStateNone());
        }
    }

}
