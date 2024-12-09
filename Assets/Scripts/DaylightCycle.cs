using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class DaylightCycle : MonoBehaviour
{
    [SerializeField] float rotationSpeed = 1f;

    private void Update()
    {
        transform.Rotate(new Vector3(-rotationSpeed * Time.deltaTime, 0f, 0f));
    }
}
