using UnityEngine;
using UnityEngine.UI;

public enum BowState
{
    None,
    Pulling,
}

public class PlayerBow : MonoBehaviour
{
    BeatManager beat;
    Hitscan Hitscan;
    public BowState state;

    float currPullBeats;
    float minPullBeats = 0.5f;

    float maxDist = 100f;
    bool lookingAtShootable;
    [SerializeField] Image reticleImg;


    void Start()
    {
        state = BowState.None;

        beat = GameObject.FindGameObjectWithTag("BeatManager").GetComponent<BeatManager>();
        beat.OnBeat.AddListener(() =>
        {
            currPullBeats++;
        });
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
                } 
                break;
            case BowState.Pulling:
                if (Input.GetMouseButtonUp(0))
                {
                    state = BowState.None;
                    if (currPullBeats >= minPullBeats && lookingAtShootable)
                    {
                        target.collider.GetComponent<IShootable>().OnShot.Invoke();
                    }
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
        IShootable target;
        if (Physics.Raycast(ray, out hit, maxDist))
        {
            if (hit.collider.GetComponent(typeof(MonoBehaviour)) is IShootable shootable)
            {
                lookingAtShootable = true;
                reticleImg.color = Color.red;
                target = shootable;
            }
        }
        else
        {
            lookingAtShootable = false;
            reticleImg.color = Color.white;
        }
        return hit;
    }


}
