using UnityEngine;

public enum ShootState
{
    Ready,
    Shooting,
    OnCooldown,
}

public class ShooterEnemy : MonoBehaviour
{
    GameObject player;
    [SerializeField]
    [Range(1, 30)]
    float maxDetectionDistance;
    [SerializeField] bool showDetectionGizmo;

    ShootState shootState;

    float shootCd;
    float shootCdMax = 3f;
    float shootCdVariance;

    [SerializeField] GameObject bulletPrefab;
    [SerializeField] BeatManager beatManager;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        shootState = ShootState.OnCooldown;
        ResetShootCooldown();
        beatManager.OnBeat.AddListener(() =>
        {
            if (BeatManager.beatNum % 4 == 0 && PlayerInDetectionRange() && CanSeePlayer())
            {
                Shoot(player.transform, Vector3.up);
                shootState = ShootState.OnCooldown;
                ResetShootCooldown();
            }
        });
    }

    private void Update()
    {

        switch (shootState)
        {
            case ShootState.Ready:

                break;
            case ShootState.OnCooldown:
                shootCd -= Time.deltaTime;
                if (shootCd < 0) shootState = ShootState.Ready;
                break;
            default:
                break;
        }
    }

    private void ResetShootCooldown()
    {
        shootCd = shootCdMax;
    }

    bool PlayerInDetectionRange()
    {
        return Vector3.Distance(player.transform.position, transform.position) < maxDetectionDistance;
    }

    bool CanSeePlayer()
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
        Vector3 toTarget = target.transform.position - transform.position;
        newBullet.SetTrajectory(toTarget);
    }

    void Shoot(Transform target, Vector3 offset)
    {
        EnemyBullet newBullet = Instantiate(bulletPrefab, transform).GetComponent<EnemyBullet>();
        Vector3 toTarget = target.transform.position - transform.position + offset;
        newBullet.SetTrajectory(toTarget);
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
