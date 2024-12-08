using UnityEngine;


public class ReticleManager : MonoBehaviour
{
    [SerializeField] GameObject chevron;

    [SerializeField] float maxTTLInBeats;
    [SerializeField] Vector3 maxDistance;

    float maxTTL;

    BeatManager bm;
    Transform reticle;

    private void Start()
    {
        bm = GetComponent<BeatManager>();
        reticle = GameObject.FindGameObjectWithTag("Reticle").transform;

        maxTTL = BeatsToSecs(maxTTLInBeats);

        BeatManager.OnBeat.AddListener(() =>
        {
            CreateNewIndicator(false);
            CreateNewIndicator(true);
        });
        bm.OnNewMusic.AddListener(() =>
        {
            maxTTL = BeatsToSecs(maxTTLInBeats);
        });
    }

    void CreateNewIndicator(bool isLeft)
    {
        GameObject indObj = Instantiate(chevron, Vector3.zero, Quaternion.identity, reticle);
        BeatIndicatorUI ind = indObj.GetComponent<BeatIndicatorUI>();
        ind.StartIndicator(maxTTL, maxDistance.x, isLeft);
    }

    void DestroyAllIndicators()
    {

    }


    float BeatsToSecs(float numBeats)
    {
        return numBeats * bm.CalcSecsPerBeat();
    }
}
