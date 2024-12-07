using UnityEngine;
using UnityEngine.Events;

public enum ShootState
{
    Ready,
    Shooting,
    OnCooldown,
}

public class ShooterEnemy : IDashable
{
    protected GameObject player;

    [SerializeField] [Range(1, 60)] float maxDetectionDistance;
    [SerializeField] bool showDetectionGizmo;
    [SerializeField] float beatCooldown=4f;

    ShootState shootState;

    float shootCd;
    float shootCdMax = 0f;
    float shootCdVariance;

    [SerializeField] GameObject bulletPrefab;

    LevelManager level;

    public UnityEvent OnExploded = new UnityEvent();
    public UnityEvent OnDefeated = new UnityEvent();

    protected virtual void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        level = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>();

        shootState = ShootState.OnCooldown;
        ResetShootCooldown();

        if (beatCooldown <= 0) beatCooldown = 4;

        BeatManager.OnBeat.AddListener(() =>
        {
            if (BeatManager.beatNum % beatCooldown == 0 && PlayerInDetectionRange() && CanSeePlayer())
            {
                Shoot(player.transform);
                shootState = ShootState.OnCooldown;
                ResetShootCooldown();
            }
        });

        OnDefeated.AddListener(() =>
        {
            // add one to elim score if player is the killer
            if (level?.sm.GetState() is LevelStateElim state)
            {
                state.OnEnemyElim.Invoke();
            }
            Die();
        });

        OnDashedInto.AddListener(OnDefeated.Invoke);
        OnExploded.AddListener(OnDefeated.Invoke);
    }

    private void Update()
    {
        switch (shootState)
        {
            default: break;
            case ShootState.Ready: break;
            case ShootState.OnCooldown:
                shootCd -= Time.deltaTime;
                if (shootCd < 0) shootState = ShootState.Ready;
                break;
        }
    }

    private void ResetShootCooldown()
    {
        shootCd = shootCdMax;
    }

    protected bool PlayerInDetectionRange()
    {
        return Vector3.Distance(player.transform.position, transform.position) < maxDetectionDistance;
    }

    protected bool CanSeePlayer()
    {
        RaycastHit hit;
        Vector3 eyePos = transform.position + Vector3.up;
        Vector3 toPlayer = player.transform.position - transform.position;
        if (Physics.Raycast(eyePos, toPlayer.normalized, out hit))
        {
            return true;
        }
        return false;
    }

    void Shoot(Transform target)
    {
        EnemyBullet newBullet = Instantiate(bulletPrefab, transform).GetComponent<EnemyBullet>();
        newBullet.transform.SetParent(null);
        Vector3 toTarget = target.transform.position - transform.position;
        newBullet.SetTrajectory(toTarget);
    }

    void Shoot(Transform target, Vector3 offset)
    {
        EnemyBullet newBullet = Instantiate(bulletPrefab, transform).GetComponent<EnemyBullet>();
        newBullet.transform.SetParent(null);
        Vector3 toTarget = target.transform.position - transform.position + offset;
        newBullet.SetTrajectory(toTarget);
    }

    void Die()
    {
        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        if (showDetectionGizmo)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, maxDetectionDistance);
        }
    }
}
