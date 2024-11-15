using StarterAssets;
using UnityEngine;

public enum GravState
{
    None,
    Normal,
    Powerup,
    Zero,
    Reversed
}

public class GravityManager : MonoBehaviour
{
    
    static GravState currGravState;
    static GravState prevGravState;

    [SerializeField] FirstPersonController player;

    // powerup timers
    float defaultPuTimerMax = 10f;
    float puTimer;

    const float g = 9.81f;

    private void Start()
    {
        currGravState = GravState.Normal;
    }

    public void SetGravity(GravState newState)
    {
        prevGravState = currGravState;
        currGravState = newState;
        float gravMult = 1f;
        switch (currGravState)
        {
            case GravState.Normal:
                gravMult = 1f;
                break;
            case GravState.Powerup:
                gravMult = 0.5f;
                break;
            case GravState.Zero:
                gravMult = 0f;
                break;
            case GravState.Reversed:
                gravMult = -1;
                break;
            default:
                gravMult = 1f;
                break;
        }
        Physics.gravity = Vector3.down * g * gravMult;
    }

    private void Update()
    {
        if (currGravState == GravState.Powerup)
        {
            if (puTimer >= 0) puTimer -= Time.deltaTime;
            else EndGravPowerup();
        }
    }

    public void StartGravPowerup(float duration = -1)
    {

        puTimer = duration != -1 ? duration : defaultPuTimerMax;
        SetGravity(GravState.Powerup);
    }

    public void EndGravPowerup()
    {
        SetGravity(prevGravState);
    }
}


