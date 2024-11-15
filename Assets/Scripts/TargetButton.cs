using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TargetButton : IShootable
{
    [SerializeField] LevelManager level;

    private void Start()
    {
        OnShot.AddListener(() =>
        {
            ChangeToRandCol();
            level.IncButtonsHit();
            level.RefreshUI();
        });
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
