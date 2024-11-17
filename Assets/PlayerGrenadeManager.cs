using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SearchService;
using UnityEngine.UI;

public enum GrenadeMode
{
    Ice,
    Bounce,
    Gravity // unlockable
}

public class PlayerGrenadeManager : MonoBehaviour
{
    [SerializeField] GameObject grenadePrefab;
    [SerializeField] float launchSpeed;

    [Header("Debug")]
    [SerializeField] TextMeshProUGUI modeText;

    PlayerInput inp;

    GrenadeMode selectedMode;

    Vector3 spawnOffset = new Vector3(-0.5f, -0.1f, 0.1f);

    private void Start()
    {
        inp = GetComponent<PlayerInput>();

        selectedMode = GrenadeMode.Ice;
        UpdateModeUIText();
    }

    private void Update()
    {
        if (inp.grenadeInp)
        {
            GameObject newGrenade = Instantiate(grenadePrefab, transform.position+(spawnOffset+transform.forward), Quaternion.Euler(45f, 45f, 45f));

            Vector3 launchDir = transform.forward;
            launchDir.y += 0.5f;
            newGrenade.GetComponent<Rigidbody>().AddForce(launchDir * launchSpeed, ForceMode.Impulse);

            newGrenade.GetComponent<Grenade>().SetMode(selectedMode);
        }
        else if (inp.grenadeModeInp)
        {
            selectedMode = NextGrenadeMode(selectedMode);
            UpdateModeUIText();
        }
    }

    GrenadeMode NextGrenadeMode(GrenadeMode mode)
    {
        return (GrenadeMode)(((int)mode + 1) % 3); //wrapz to 0
    }

    void UpdateModeUIText()
    {
        modeText.text = "mode: " + selectedMode.ToString();
    }
}
