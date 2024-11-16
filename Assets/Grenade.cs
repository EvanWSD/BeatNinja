using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Grenade : IDashable
{
    int maxBeatLifetime = 4;
    float beatsLeft;
    float explosionRadius = 1f;

    BeatManager beatManager;

    private void Awake()
    {
        beatsLeft = maxBeatLifetime;
    }

    private void Start()
    {
        beatManager = GameObject.FindGameObjectWithTag("BeatManager").GetComponent<BeatManager>();
        beatManager.OnBeat.AddListener(() =>
        {
            beatsLeft -= 1;
            if (beatsLeft <= 0)
            {
                Explode();
            }
        });
    }

    void Explode()
    {
        Destroy(gameObject);
    }
}
