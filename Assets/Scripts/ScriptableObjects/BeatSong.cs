using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BeatSong", menuName = "BeatNinja/BeatSong")]
public class BeatSong : ScriptableObject
{
    public AudioClip audioClip;
    public float bpm;
}
