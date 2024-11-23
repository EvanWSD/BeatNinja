using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DeathPlane : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlrCheckpointManager plrCpManager = other.GetComponent<PlrCheckpointManager>();
            other.transform.position = plrCpManager.lastCp.position;
            // TODO: death
        }
    }
}
 