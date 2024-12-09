using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public enum PowerupState
{
    ready,
    onCooldown,
    deactivated
}

public class GravPowerupCube : MonoBehaviour
{
    [SerializeField] float powerupTimer;
    [SerializeField] float timerVariance;

    [SerializeField] Material readyMat;
    [SerializeField] Material cdMat;

    [SerializeField] GravityManager gravManager;
    PowerupState state;

    [SerializeField] Volume ppVolume;
    Vignette vignette;

    float cdMax = 10f;
    float cdTimer;

    MeshRenderer mesh;


    private void Start()
    {
        ppVolume = GameObject.FindGameObjectWithTag("GravVignette").GetComponent<Volume>();
        gravManager = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<GravityManager>();
        mesh = GetComponent<MeshRenderer>();
        ppVolume.sharedProfile.TryGet(out vignette);
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player") && state == PowerupState.ready)
        {
            state = PowerupState.onCooldown;
            cdTimer = cdMax;
            mesh.material = cdMat;
            StartCoroutine(EnableVignette());
            float duration = powerupTimer + Random.Range(-timerVariance, timerVariance);
            gravManager.StartGravPowerup(duration);
        }

    }

    private void Update()
    {
        if (state == PowerupState.onCooldown)
        {
            cdTimer -= Time.deltaTime;
            if (cdTimer <= 0)
            {
                state = PowerupState.ready;
                mesh.material = readyMat;
                StartCoroutine(DisableVignette());
            }
        }
    }

    IEnumerator EnableVignette(float transDuration=1f)
    {
        StopCoroutine(DisableVignette());
        float transTimer = 0f;
        vignette.color.Override(new Color(0.21f, 0f, 0.88f));
        vignette.smoothness.Override(0.571f);
        while (transTimer <= transDuration)
        {
            vignette.intensity.Override(Mathf.Lerp(0, 0.283f, transTimer / transDuration));
            transTimer += Time.deltaTime;
            yield return null;
        }

    }

    IEnumerator DisableVignette(float transDuration=1f)
    {
        StopCoroutine(EnableVignette());
        float transTimer = 0f;
        while (transTimer <= transDuration)
        {
            vignette.intensity.Override(Mathf.Lerp(0.283f, 0, transTimer / transDuration));
            transTimer += Time.deltaTime;
            yield return null;
        }
    }
}
