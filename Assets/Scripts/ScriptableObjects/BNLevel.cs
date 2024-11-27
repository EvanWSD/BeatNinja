using UnityEngine;

[CreateAssetMenu(fileName = "LevelData", menuName = "BeatNinja/BNLevelData", order = 1)]
public class BNLevelData : ScriptableObject
{
    public int lvlNum;
    public BeatSong song;
}
