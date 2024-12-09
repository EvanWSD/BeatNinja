using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.UI.Image;

public enum BowState
{
    None,
    Pulling,
}

public class PlayerBow : MonoBehaviour
{
    public BowState state;

    float beatNumWhenStarted;
    float currPullBeats;
    float minPullBeats = 0.5f;

    [SerializeField] LayerMask shootableTriggers;

    float maxDist = 100f;
    bool lookingAtShootable;
    [SerializeField] Image reticleImg;

    [SerializeField] GameObject bowObj;

    float baseScale;
    [SerializeField] float maxScaleDelta;

    BeatManager beat;

    void Start()
    {
        beat = GameObject.FindGameObjectWithTag("BeatManager").GetComponent<BeatManager>();

        state = BowState.None;
        baseScale = bowObj.transform.localScale.x;

    }

    void Update()
    {
        RaycastHit target = PerformHitscan();
        switch (state)
        {
            case BowState.None:
                if (Input.GetMouseButtonDown(0))
                {
                    state = BowState.Pulling;
                    currPullBeats = 0;
                    beatNumWhenStarted = beat.beatNumLerp;
                } 
                break;
            case BowState.Pulling:
                currPullBeats = beat.beatNumLerp - beatNumWhenStarted;
                LerpBowScale();
                if (Input.GetMouseButtonUp(0))
                {
                    AttemptShot(target);
                    ResetBowScale();
                }
                break;
            default:
                state = BowState.None;
                break;
        }
    }

    RaycastHit PerformHitscan()
    {
        
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

        RaycastHit hit;
        RaycastHit triggerHit;
        IShootable target;
        bool triggerRaycast = Physics.Raycast(ray, out triggerHit, maxDist, shootableTriggers, QueryTriggerInteraction.Collide);
        if (Physics.Raycast(ray, out hit, maxDist) || triggerRaycast)
        {
            if (triggerRaycast) hit = triggerHit;
            if (hit.collider.GetComponent(typeof(MonoBehaviour)) is IShootable shootable)
            {
                lookingAtShootable = true;
                reticleImg.color = Color.red;
                target = shootable;
            }
            else
            {
                reticleImg.color = Color.white;
                lookingAtShootable = false;
            }
        }
        return hit;
    }

    void AttemptShot(RaycastHit target)
    {
        state = BowState.None;
        if (beat.IsCalledNearBeat())
        {
            if (currPullBeats >= minPullBeats && lookingAtShootable)
            {
                target.collider.GetComponent<IShootable>().OnShot.Invoke();
            }
        }
    }

    void LerpBowScale()
    {
        Vector3 scale = bowObj.transform.localScale;
        scale.x = Mathf.Lerp(baseScale, baseScale+maxScaleDelta, currPullBeats / minPullBeats);
        bowObj.transform.localScale = scale;
    }

    void ResetBowScale()
    {
        Vector3 scale = bowObj.transform.localScale;
        bowObj.transform.localScale = new Vector3(baseScale, scale.y, scale.z);
    }
}
