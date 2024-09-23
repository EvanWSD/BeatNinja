using UnityEngine;
using UnityEngine.Events;


// Manages the timing of code to the beat of the music (using manually input bpm)
public class BeatManager : MonoBehaviour
{

    float bpm = 105f;

    [SerializeField] AudioSource musicSource;

    // For keeping time with the bpm
    float speedMult = 1f;
    float beatNum = 1;
    float timeSinceLastBeat = 0f;
    public float secsPerQuestion { get; private set; }
    const float syncFix = 0.1f; // AudioSources in Unity have a slight unavoidable delay

    // call from other scripts
    public UnityEvent OnBeat = new UnityEvent();



    void Start()
    {
        secsPerQuestion = CalcSecsPerQuestion();
        OnBeat.AddListener(() => beatNum++);
    }

    // executes before all other Update methods.
    void Update()
    {
        // invokes OnBeat in time with the music
        timeSinceLastBeat = (musicSource.time - syncFix) / beatNum;
        if (timeSinceLastBeat >= secsPerQuestion)
        {
            OnBeat.Invoke();
        }
    }


    public float CalcSecsPerQuestion()
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
}