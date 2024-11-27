using UnityEngine;

// 'empty' state for interim sections with no goal
// e.g. first few seconds of a level
public class LevelStateNone : ILevelState
{
    public override void Enter() { }
    public override void Update() { }
    public override void Exit() { }
    public bool CheckWinCondition() { return false; }
}
