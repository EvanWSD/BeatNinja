using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] int checkpointIndex = 1;
    [SerializeField] bool atNewSection;
    [SerializeField] protected SectionType nextState; // section type once plr reaches here 
    [SerializeField] GameObject endOfSectionDoor;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlrCheckpointManager player = other.GetComponent<PlrCheckpointManager>();
            player.OnCheckpointReached(checkpointIndex, transform, atNewSection, endOfSectionDoor, nextState);
        }
    }
}
