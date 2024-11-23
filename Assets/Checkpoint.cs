using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{

    [SerializeField] int checkpointIndex = 1;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlrCheckpointManager>().UpdateCheckpointNum(checkpointIndex, transform);

        }
    }
}
