using System.Collections;
using UnityEngine;

public class Grenade : IDashable
{
    int maxBeatLifetime = 4;
    float beatsLeft;
    float explosionRadius = 1f;
    [SerializeField] GameObject explosionSphere;

    Rigidbody rb;
    MeshRenderer mesh;

    BeatManager beatManager;

    private void Awake()
    {
        beatsLeft = maxBeatLifetime;
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        mesh = GetComponent<MeshRenderer>();

        beatManager = GameObject.FindGameObjectWithTag("BeatManager").GetComponent<BeatManager>();
        beatManager.OnBeat.AddListener(() =>
        {
            beatsLeft -= 1;
            if (beatsLeft <= 0)
            {
                StartCoroutine(Explode());
            }
        });
    }

    IEnumerator Explode()
    {
        explosionSphere.SetActive(true);
        explosionSphere.transform.localScale = Vector3.one * explosionRadius * 10f;
        rb.constraints = RigidbodyConstraints.FreezeAll;
        mesh.enabled = false;
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }
}
