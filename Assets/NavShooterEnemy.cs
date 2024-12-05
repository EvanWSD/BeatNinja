using UnityEngine.AI;

public class NavShooterEnemy : ShooterEnemy
{
    NavMeshAgent navAgent;

    protected override void Start()
    {
        base.Start();
        navAgent = GetComponent<NavMeshAgent>();
    }

    private void FixedUpdate()
    {
        if (PlayerInDetectionRange() && CanSeePlayer())
        {
            navAgent.SetDestination(player.transform.position);
        }
    }
}