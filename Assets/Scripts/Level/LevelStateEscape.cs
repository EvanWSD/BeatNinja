using UnityEngine;

public class LevelStateEscape : ILevelState
{
    [SerializeField] float timeToEscapeMax;
    float escapeTimer;

    public override void Enter()
    {
        escapeTimer = timeToEscapeMax;
    }

    public override void Update() {
        escapeTimer -= Time.deltaTime;
        if (escapeTimer <= 0)
        {
            // die/restart
        }
    }
    public override void Exit() { }

    public override bool CheckWinCondition() { return false; }


}
