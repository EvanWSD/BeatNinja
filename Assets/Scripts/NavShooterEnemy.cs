using UnityEngine;
using UnityEngine.AI;

public enum MoverState
{
    Offensive,
    Defensive
}

public class NavShooterEnemy : ShooterEnemy
{
    NavMeshAgent navAgent;

    float tacticTimerMax = 3f;
    float tacticTimerVariance = 1f;
    float tacticTimer = 0f;

    float forceDefenseDistance = 5f;

    MoverState state;

    protected override void Start()
    {
        base.Start();
        state = MoverState.Offensive;
        navAgent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        tacticTimer -= Time.deltaTime;
        if (tacticTimer <= 0)
        {
            ResetTacticTimer();
            state = nextMoverState(state);
        }
    }

    private void FixedUpdate()
    {
        if (PlayerInDetectionRange() && CanSeePlayer())
        {
            switch (state)
            {
                case MoverState.Offensive:
                    navAgent.SetDestination(player.transform.position);
                    break;
                case MoverState.Defensive:
                    navAgent.SetDestination(player.transform.position - transform.position);
                    break;
                default:
                    navAgent.SetDestination(player.transform.position);
                    break;
            }

            Vector3 toPlayer = player.transform.position - transform.position;
            if (toPlayer.magnitude < forceDefenseDistance)
            {
                state = MoverState.Defensive;
                tacticTimer = 3f;
            }
        }
    }

    void ResetTacticTimer()
    {
        tacticTimer = tacticTimerMax + Random.Range(-tacticTimerVariance, tacticTimerVariance);
    }

    MoverState nextMoverState(MoverState state)
    {
       return (MoverState)(((int)state + 1) % 2);
    }

}