using UnityEngine;

public class ElimCheckpoint : Checkpoint
{
    [SerializeField] int numEnemiesTarget;

    private void Start()
    {
        nextState = SectionType.Elims;
    }
}
