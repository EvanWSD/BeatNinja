using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TargetButton : IShootable
{
    LevelManager level;

    private void Start()
    {
        level = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>();

        OnShot.AddListener(() =>
        {
            ChangeToRandCol();
            IncIfButtonSection();
        });
    }

    void IncIfButtonSection()
    {
        if (level?.sm.GetState() is LevelStateButton state)
        {
            state.OnLvlButtonHit.Invoke();
        }
    }

    void ChangeToRandCol()
    {
        Material mat = GetComponent<MeshRenderer>().material;
        float r = Random.Range(0, 100f) / 100f;
        float g = Random.Range(0, 100f) / 100f;
        float b = Random.Range(0, 100f) / 100f;
        mat.color = new Color(r, g, b);
    }
}
