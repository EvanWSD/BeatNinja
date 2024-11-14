using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DashManager : MonoBehaviour
{
    public UnityEvent OnGoodDash = new UnityEvent();

    private void OnTriggerEnter(Collider other)
    {
        OnGoodDash.Invoke();
    }
}
