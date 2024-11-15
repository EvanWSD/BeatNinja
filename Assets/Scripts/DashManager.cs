using UnityEngine;
using UnityEngine.Events;

public class DashManager : MonoBehaviour
{
    [SerializeField] LayerMask dashTargetLayers;
    [HideInInspector] public UnityEvent OnGoodDash = new UnityEvent();

    private void Start()
    {
        GetComponent<Collider>().enabled = false;
    }


    void OnTriggerEnter(Collider other)
    {
        if (isOnDashableLayer(other))
        {
            OnGoodDash.Invoke(); // player
            if (other.TryGetComponent(out IDashable dashable))
            {
                dashable.OnDashedInto.Invoke();    
            }
        }
    }

    bool isOnDashableLayer(Collider col)
    {
        return (dashTargetLayers.value & (1 << col.gameObject.layer)) > 0;
    }


}
