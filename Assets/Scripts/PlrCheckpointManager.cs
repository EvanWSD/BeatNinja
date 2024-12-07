using UnityEngine;

public class PlrCheckpointManager : MonoBehaviour
{
    public int currCpNum { get; private set; } = 1;
    public Transform lastCp { get; private set; }
    LevelManager level;

    [SerializeField] GameObject initCheckpoint;

    private void Start()
    {
        level = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>();
        lastCp = initCheckpoint.transform;
    }

    public void OnCheckpointReached(int newIndex, Transform cpTransform, bool isNewSection, GameObject endOfSectionDoor, SectionType sectionType = SectionType.None)
    {
        if (newIndex > currCpNum)
        {
            currCpNum = newIndex;
            lastCp = cpTransform;
            if (isNewSection)
            {
                level.sm.SetLevelStateFromEnum(sectionType);
                level.currSectionEndDoor = endOfSectionDoor;
            }
        }
    }
}
