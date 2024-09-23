using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// idk could be used to detect special object being shot
[RequireComponent(typeof(Collider))]
public class IShootable : MonoBehaviour
{
    public UnityEvent OnShot;

}
