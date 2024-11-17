using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugBeatSquare : MonoBehaviour
{
    Image img;
    Color goodColor = Color.green;
    Color badColor = Color.red;

    BeatManager beat;

    private void Start()
    {
        beat = GameObject.FindGameObjectWithTag("BeatManager").GetComponent<BeatManager>();
        img = GetComponent<Image>();
    }

    private void Update()
    {
        img.color = beat.IsCalledNearBeat() ? goodColor : badColor;
    }
}
