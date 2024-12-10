using TMPro;
using UnityEngine;

public class TimedChallengeDoor : MonoBehaviour
{
    float timeDoorTimer;
    bool isDoorOpen = true;
    [SerializeField] float challengeTimeLimit = 60f;
    [SerializeField] Transform closedPosition;
    [SerializeField] TextMeshPro doorText;

    void OnAwake()
    {
        timeDoorTimer = 0f;
    }

    void Update()
    {
        if (isDoorOpen)
        {
            timeDoorTimer += Time.deltaTime;
            float timeLeft = challengeTimeLimit - timeDoorTimer;
            doorText.text = FormatTimer(timeLeft);
            if (timeDoorTimer >= challengeTimeLimit)
            {
                CloseDoor();
            }
        }
    }

    void CloseDoor()
    {
        isDoorOpen = false;
        transform.position = closedPosition.position;
        doorText.text = FormatTimer(challengeTimeLimit);
    }

    string FormatTimer(float t)
    {
        return $"{t:00.00}";
    }
}
