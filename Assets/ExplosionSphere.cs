using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ExplosionSphere : MonoBehaviour
{
    [SerializeField] LayerMask damageableLayers;
    float impulseForce;

    void OnTriggerEnter(Collider other)
    {
        if (onDamageableLayer(other))
        {
            if (other.TryGetComponent(out Rigidbody rb))
            {
                Vector3 dirToOther = (other.transform.position - transform.position).normalized;
                rb.AddForce(dirToOther * impulseForce, ForceMode.Impulse);
            }
        }
    }

    bool onDamageableLayer(Collider other)
    {
        return (damageableLayers.value & (1 << other.gameObject.layer)) != 0;
    }
}
