using System.Collections;
using System.Linq;
using UnityEngine;

public class Grenade : IDashable
{
    int maxBeatLifetime = 5;
    float beatsLeft;
    float explosionRadius = 10f;
    bool isExploding;
    [SerializeField] LayerMask explodableMask;

    GrenadeMode mode;
    [SerializeField] GameObject explosionSphere;

    Material normalMat;
    [Header("Materials")]
    [SerializeField] Material iceMat;
    [SerializeField] Material bounceMat;
    [SerializeField] Material gravMat;
    [SerializeField] Material beepMat;

    [Header("Physic Materials")]
    [SerializeField] PhysicMaterial icePMat;
    [SerializeField] PhysicMaterial bouncePMat;
    //(grav uses default/None)

    Rigidbody rb;
    MeshRenderer mesh;
    Collider col;

    void Awake()
    {
        beatsLeft = maxBeatLifetime;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        mesh = GetComponent<MeshRenderer>();
        col = GetComponent<Collider>();

        BeatManager.OnBeat.AddListener(() =>
        {
            beatsLeft -= 1;
            if (beatsLeft <= 0 && !isExploding)
            {
                isExploding = true;
                StartCoroutine(Explode());
            }
        });

        ApplyModeChanges(mode);

    }

    void ApplyModeChanges(GrenadeMode mode)
    {
        switch (mode)
        {
            case GrenadeMode.Ice:
                col.material = icePMat;
                mesh.material = normalMat = iceMat;
                break;
            case GrenadeMode.Bounce:
                col.material = bouncePMat;
                mesh.material = normalMat = bounceMat;
                break;
            case GrenadeMode.Gravity:
                col.material = null;
                mesh.material = normalMat = gravMat;
                break;
            default:
                break;
        }
    }

    public void SetMode(GrenadeMode mode)
    {
        this.mode = mode;
    }

    void FixedUpdate()
    {
        mesh.material = BeatManager.IsCalledNearBeat(0.1f) ? beepMat : normalMat;
    }

    IEnumerator Explode()
    {
        explosionSphere.SetActive(true);
        explosionSphere.transform.localScale = Vector3.one * explosionRadius * 5f;
        rb.constraints = RigidbodyConstraints.FreezeAll;
        mesh.enabled = false;
        DestroyObjectsInRadius();
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }

    void DestroyObjectsInRadius()
    {
        Collider[] allNearbyExplodables = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider col in allNearbyExplodables.Where(IsInLOS))
        {
            if (col.CompareTag("Enemy"))
            {
                col.TryGetComponent(out ShooterEnemy enemy);
                enemy?.OnExploded.Invoke();
            }
        }
    }

    bool IsInLOS(Collider col)
    {
        return Physics.Raycast(transform.position, col.transform.position - transform.position, explosionRadius);
    }
}
