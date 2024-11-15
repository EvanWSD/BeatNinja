using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// idk could be used to detect special object being shot
[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
public class IDashable : MonoBehaviour
{
    public UnityEvent OnDashedInto;
}
