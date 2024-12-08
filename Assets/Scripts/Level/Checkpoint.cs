using UnityEngine;
using UnityEngine.Events;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] public int checkpointIndex = 1;
    [SerializeField] public bool atNewSection;
    [SerializeField] public SectionType nextState;
    [SerializeField] public GameObject endOfSectionDoor;

    public UnityEvent OnThisCheckpointReached = new UnityEvent();

    void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlrCheckpointManager player))
        {
            if (player.TryNewCheckpoint(this)) // if checkpoint is new to plr
            {
                OnThisCheckpointReached.Invoke();
            }
        }
    }
}
