using UnityEngine;
using UnityEngine.Events;

public class PlrDeath : MonoBehaviour
{
    PlrCheckpointManager cpManager;
    BasicPlayerMovement movement;

    public UnityEvent OnPlayerDeath = new UnityEvent();

    private void Start()
    {
        movement = GetComponent<BasicPlayerMovement>();
        cpManager = GetComponent<PlrCheckpointManager>();
        OnPlayerDeath.AddListener(() =>
        {
            transform.position = cpManager.lastCp.position;
        });
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("EnemyBullet"))
        {
            if (!GameManager.Global_GodMode && !movement.RecentlyDashed()) 
                OnPlayerDeath.Invoke();
        }
    }
}
