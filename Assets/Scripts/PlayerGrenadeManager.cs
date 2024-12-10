using TMPro;
using UnityEngine;

public enum GrenadeMode
{
    Ice,
    Bounce,
    Gravity
}

public class PlayerGrenadeManager : MonoBehaviour
{
    [SerializeField] GameObject grenadePrefab;
    [SerializeField] float launchSpeed;

    [Header("Debug")]
    [SerializeField] TextMeshProUGUI modeText;

    PlayerInput inp;
    BeatManager beat;

    int grenadeBeatCDMax = 7; // every 2 bars of 4:4 song
    int grenadeBeatCD;

    GrenadeMode selectedMode;

    Vector3 spawnOffset = new Vector3(-0.5f, -0.1f, 0.1f);

    private void Start()
    {
        inp = GetComponent<PlayerInput>();
        beat = GameObject.FindGameObjectWithTag("BeatManager").GetComponent<BeatManager>();

        selectedMode = GrenadeMode.Ice;
        UpdateModeUIText();

        beat.OnBeat.AddListener(() => { grenadeBeatCD--; });
    }

    private void Update()
    {
        if (inp.grenadeInp && grenadeBeatCD <= 0 && beat.IsCalledNearBeat())
        {
            GameObject newGrenade = Instantiate(grenadePrefab, transform.position+(spawnOffset+transform.forward), Quaternion.Euler(45f, 45f, 45f));

            Vector3 launchDir = transform.forward;
            launchDir.y += 0.5f;
            newGrenade.GetComponent<Rigidbody>().AddForce(launchDir * launchSpeed, ForceMode.Impulse);

            newGrenade.GetComponent<Grenade>().SetMode(selectedMode);

            grenadeBeatCD = grenadeBeatCDMax;
        }
        else if (inp.grenadeModeInp)
        {
            selectedMode = NextGrenadeMode(selectedMode);
            UpdateModeUIText();
        }
    }

    GrenadeMode NextGrenadeMode(GrenadeMode mode)
    {
        return (GrenadeMode)(((int)mode + 1) % 2); //wraps to 0, mod 3 to reenable grav grenade
    }

    void UpdateModeUIText()
    {
        modeText.text = "mode: " + selectedMode.ToString();
    }
}
