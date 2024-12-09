using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
public class IDashable : MonoBehaviour
{
    public UnityEvent OnDashedInto;
}
