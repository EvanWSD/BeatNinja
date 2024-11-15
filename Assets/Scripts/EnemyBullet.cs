using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    float spd=10f;
    Vector3 dir;
    
    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        rb.velocity = dir.normalized * spd;
    }

    public void SetTrajectory(Vector3 dir, float spd=-1)
    {
        this.dir = dir.normalized;
    }

    void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
    }
}
