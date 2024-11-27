using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] int checkpointIndex = 1;
    [SerializeField] bool atNewSection;
    [SerializeField] SectionType nextState; // section type once plr reaches here 

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlrCheckpointManager player = other.GetComponent<PlrCheckpointManager>();
            player.OnCheckpointReached(checkpointIndex, transform, atNewSection, nextState);
        }
    }
}
