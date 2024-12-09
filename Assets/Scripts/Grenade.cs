using System.Collections;
using System.Linq;
using UnityEngine;

public class Grenade : IDashable
{
    int maxBeatLifetime = 5;
    float beatsLeft;
    float explosionRadius = 10f;
    float explosionDuration = 1f;
    float maxExplosionSizeTime = 0.15f;
    float currExplosionTime;
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
    BeatManager beat;

    void Awake()
    {
        beatsLeft = maxBeatLifetime;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        mesh = GetComponent<MeshRenderer>();
        col = GetComponent<Collider>();
        beat = GameObject.FindGameObjectWithTag("BeatManager").GetComponent<BeatManager>();

        ApplyModeChanges(mode);

        beat.OnBeat.AddListener(() =>
        {
            beatsLeft -= 1;
            if (beatsLeft <= 0 && !isExploding)
            {
                StartCoroutine(Explode());
            }
        });

    }

    private void Update()
    {
        if (isExploding)
        {
            currExplosionTime += Time.deltaTime;
            LerpExplosionSize();
        }
    }

    void LerpExplosionSize()
    {
        Vector3 maxScale = Vector3.one * explosionRadius * 5f;
        Vector3 currScale = maxScale * currExplosionTime / maxExplosionSizeTime;
        currScale = Vector3.Min(currScale, maxScale);
        explosionSphere.transform.localScale = currScale;
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
        mesh.material = beat.IsCalledNearBeat(0.1f) ? beepMat : normalMat;
    }

    public IEnumerator Explode()
    {
        isExploding = true;
        currExplosionTime = 0f;
        ReplaceWithExplosionMesh();
        rb.constraints = RigidbodyConstraints.FreezeAll;
        ExplodeInRadiusAndLOS();
        yield return new WaitForSeconds(explosionDuration);
        Destroy(this.gameObject);
    }

    void ReplaceWithExplosionMesh()
    {
        explosionSphere.SetActive(true);
        mesh.enabled = false;
    }

    void ExplodeInRadiusAndLOS()
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
