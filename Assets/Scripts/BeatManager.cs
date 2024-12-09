using UnityEngine;
using UnityEngine.Events;


// Manages the timing of code to the beat of the music (using manually input bpm)
public class BeatManager : MonoBehaviour
{
    public float bpm { get; private set; } = 115f ;

    [SerializeField] AudioSource musicSource;

    [Tooltip("+- beat progression for which timed inputs count as 'pressed on a beat'")]
    [SerializeField] float validBeatInputVariance = 0.2f;

    float speedMult = 1f;
    public float beatNum = 1;
    public float beatNumLerp;
    public float currentBeatProgress01;
    float timeSinceLastBeat = 0f;
    float timeOfLastBeat=0f;
    public float secsPerBeat { get; private set; }
    const float syncFix = 0.1f; // AudioSources in Unity have a slight unavoidable delay
    
    public UnityEvent OnBeat = new UnityEvent();
    public UnityEvent OnNewMusic = new UnityEvent();

    void Start()
    {
        secsPerBeat = CalcSecsPerBeat();
        OnNewMusic.Invoke();
        OnBeat.AddListener(() => { 
            beatNum++;
            timeOfLastBeat = GetMusicTimeElapsed();
        });
    }

    // executes before all other Update methods.
    void Update()
    {
        // invokes OnBeat in time with the music
        currentBeatProgress01 = timeSinceLastBeat / secsPerBeat;
        beatNumLerp = beatNum + currentBeatProgress01;
        timeSinceLastBeat = GetMusicTimeElapsed() - timeOfLastBeat;
        if (timeSinceLastBeat >= secsPerBeat)
        {
            OnBeat.Invoke();
        }

        // loop song if over
        if (musicSource.time >= musicSource.clip.length - 0.1f)
        {
            NewSong(musicSource.clip, this.bpm);
        }

        CanvasDebugText.Write(beatNum, "beatNum");
    }

    // used to gauge whether inputs are done in time with the music
    // greater variance means stricter timing
    public bool IsCalledNearBeat(float variance=-1)
    {
        if (variance == -1) variance = validBeatInputVariance;
        return currentBeatProgress01 <= variance || currentBeatProgress01 >= 1-variance;
    }


    public void NewSong(AudioClip newMusic, float bpm)
    {
        OnNewMusic.Invoke();
        this.bpm = bpm;
        secsPerBeat = CalcSecsPerBeat();
        musicSource.clip = newMusic;
        musicSource.Stop();
        musicSource.Play();
        beatNum = 1;
        OnBeat.Invoke();
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