using UnityEngine;

public class PlrCheckpointManager : MonoBehaviour
{
    public int currCpNum { get; private set; } = 1;
    public Transform lastCp { get; private set; }

    public void UpdateCheckpointNum(int newIndex, Transform cpTransform)
    {
        currCpNum = Mathf.Max(currCpNum, newIndex);
        lastCp = cpTransform;
    }
}
