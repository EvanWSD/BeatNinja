using UnityEngine;

public class NewMusicCheckpoint : MonoBehaviour
{
    BeatManager beat;
    Checkpoint cp;

    [SerializeField] public AudioClip newSong;
    [SerializeField] float newBpm;

    private void Start()
    {
        beat = GameObject.FindGameObjectWithTag("BeatManager").GetComponent<BeatManager>();
        cp = GetComponent<Checkpoint>();
        cp.OnThisCheckpointReached.AddListener(() =>
        {
            //beat.NewSong(newSong, newBpm);
        });
    }
}
