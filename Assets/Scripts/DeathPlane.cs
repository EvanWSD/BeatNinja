using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DeathPlane : MonoBehaviour
{
    [SerializeField] Transform respawnPoint;
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // TODO: death ?? ominous
            other.transform.position = respawnPoint.position;
        }
    }
}
 