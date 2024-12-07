using UnityEngine;
using UnityEngine.Events;

public class PlrDeath : MonoBehaviour
{
    PlrCheckpointManager cpManager;
    [SerializeField] bool godMode;

    public UnityEvent OnPlayerDeath = new UnityEvent();

    private void Start()
    {
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
            if (!godMode) OnPlayerDeath.Invoke();
        }
    }
}
