using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class IShootable : MonoBehaviour
{
    [HideInInspector]
    public UnityEvent OnShot;
}
