using UnityEngine;
using UnityEngine.Events;

public class PlrDeath : MonoBehaviour
{
    PlrCheckpointManager cpManager;
    BasicPlayerMovement movement;

    GameObject[] allEnemies;

    public UnityEvent OnPlayerDeath = new UnityEvent();

    private void Start()
    {
        movement = GetComponent<BasicPlayerMovement>();
        cpManager = GetComponent<PlrCheckpointManager>();
        allEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        OnPlayerDeath.AddListener(() =>
        {
            transform.position = cpManager.lastCp.position;
            RespawnAllEnemies();
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

    void RespawnAllEnemies()
    {
        foreach (GameObject enemy in allEnemies)
        {
            enemy.SetActive(true);
        }
    }
}
