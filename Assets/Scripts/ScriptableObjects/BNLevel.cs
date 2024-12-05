using System;
using System.Collections.Generic;
using UnityEngine;

public struct ScriptableLvlSection
{
    SectionType type;
    float data; // timer, num buttons to hit, num enemies to elim
}

[CreateAssetMenu(fileName = "LevelData", menuName = "BeatNinja/BNLevelData", order = 2)]
public class BNLevelData : ScriptableObject
{
    public int lvlNum;
    public BeatSong song;
}
