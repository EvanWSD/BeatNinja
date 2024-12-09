using UnityEngine;
using UnityEngine.Events;

public class DashManager : MonoBehaviour
{
    [SerializeField] LayerMask dashTargetLayers;
    [HideInInspector] public UnityEvent OnGoodDash = new UnityEvent();

    [SerializeField] float checkSphereRadius = 5f;

    private void Start()
    {
        GetComponent<Collider>().enabled = false;
    }


    void OnTriggerEnter(Collider other)
    {
        if (isOnDashableLayer(other))
        {
            OnGoodDash.Invoke();
            if (other.TryGetComponent(out IDashable dashable))
            {
                dashable.OnDashedInto.Invoke();
                if (!dashable.CompareTag("Enemy"))
                {
                    AlsoDashNearbyEnemies(dashable);
                }
            }
        }
    }

    void AlsoDashNearbyEnemies(IDashable dashTarget)
    {
        Collider[] nearby = Physics.OverlapSphere(dashTarget.transform.position, checkSphereRadius, dashTargetLayers);
        foreach(Collider col in nearby)
        {
            col.TryGetComponent(out IDashable dashable);
            dashable?.OnDashedInto.Invoke();
        }
    }

    bool isOnDashableLayer(Collider col)
    {
        return (dashTargetLayers.value & (1 << col.gameObject.layer)) > 0;
    }

    private void OnCollisionEnter(Collision collision)
    {
        OnTriggerEnter(collision.gameObject.GetComponent<Collider>());
    }

}
