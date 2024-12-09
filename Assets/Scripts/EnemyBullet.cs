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
        if (spd != -1)
            this.spd = spd;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(this.gameObject);
    }
}
