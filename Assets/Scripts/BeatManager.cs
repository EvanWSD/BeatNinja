using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;


// Manages the timing of code to the beat of the music (using manually input bpm)
public class BeatManager : MonoBehaviour
{

    float bpm = 105f;

    [SerializeField] AudioSource musicSource;

    [Tooltip("+- beat progression for which timed inputs count as 'pressed on a beat'")]
    [SerializeField] float validBeatInputVariance = 0.2f;

    // For keeping time with the bpm
    float speedMult = 1f;
    public static float beatNum = 1;
    public static float beatNumLerp;
    public static float currentBeatProgress01;
    float timeSinceLastBeat = 0f;
    float timeOfLastBeat=0f;
    public float secsPerBeat { get; private set; }
    const float syncFix = 0.1f; // AudioSources in Unity have a slight unavoidable delay
    

    // call from other scripts
    public UnityEvent OnBeat = new UnityEvent();

    void Start()
    {
        secsPerBeat = CalcSecsPerBeat();
        OnBeat.AddListener(() => { 
            beatNum++;
            timeOfLastBeat = GetMusicTimeElapsed();
        });
    }

    // executes before all other Update methods.
    void Update()
    {
        // invokes OnBeat in time with the music
        // timeSinceLastBeat = (musicSource.time - syncFix) / beatNum;
        currentBeatProgress01 = timeSinceLastBeat / secsPerBeat;
        beatNumLerp = beatNum + currentBeatProgress01;
        timeSinceLastBeat = GetMusicTimeElapsed() - timeOfLastBeat;
        if (timeSinceLastBeat >= secsPerBeat)
        {
            OnBeat.Invoke();
        }

        CanvasDebugText.Write(beatNum, "beatNum");
    }

    // used to gauge whether inputs are done in time with the music
    // greater variance means stricter timing
    public bool IsCalledNearBeat(float variance=-1)
    {
        if (variance == -1)
            variance = validBeatInputVariance;
        return (currentBeatProgress01 <= variance || currentBeatProgress01 >= 1-variance);
    }


    public float CalcSecsPerBeat()
    {
        return (60 / bpm) / speedMult;
    }

    public void StopMusic()
    {
        musicSource.Stop();
    }

    public void StartMusic()
    {
        musicSource.pitch *= speedMult;
        musicSource.Play();
    }

    float GetMusicTimeElapsed()
    {
        return musicSource.time - syncFix;
    }
}