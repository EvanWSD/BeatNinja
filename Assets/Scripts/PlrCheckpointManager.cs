using UnityEngine;

public class PlrCheckpointManager : MonoBehaviour
{
    public int currCpNum { get; private set; } = 0;
    public Transform lastCp { get; private set; }
    LevelManager level;

    [SerializeField] GameObject initCheckpoint;

    private void Start()
    {
        level = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>();
        lastCp = initCheckpoint.transform;
    }

    public bool TryNewCheckpoint(Checkpoint cp)
    {
        if (cp.checkpointIndex > currCpNum)
        {
            currCpNum = cp.checkpointIndex;
            lastCp = cp.transform;
            if (cp.atNewSection)
            {
                level.sm.SetLevelStateFromEnum(cp.nextState);
                level.currSectionEndDoor = cp.endOfSectionDoor;
            }
            return true;
        }
        else return false;
    }
}