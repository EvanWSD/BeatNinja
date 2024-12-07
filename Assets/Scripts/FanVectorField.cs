using UnityEngine;

public class FanVectorField : MonoBehaviour
{
    [SerializeField] Vector3 pushDir = Vector3.up;
    [SerializeField] float fanStrength = 1f;

    private void OnTriggerStay(Collider other)
    {
        other.TryGetComponent(out Rigidbody rb);
        rb.AddForce(pushDir * fanStrength, ForceMode.VelocityChange);
    }
}