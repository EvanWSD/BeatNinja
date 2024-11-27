using UnityEngine;
using UnityEngine.UI;

public class BeatIndicatorUI : MonoBehaviour
{
    bool isLeft;

    bool isStarted;
    float ttlMax;
    float ttl;
    float maxDistance;

    // two squares make up reticle image
    Image[] images;

    private void Awake()
    {
        ttl = ttlMax;
        RotateIfLeft();
    }

    private void Start()
    {
        images = GetComponentsInChildren<Image>();
    }

    private void Update()
    {
        if (isStarted)
        {
            Vector3 pos = transform.localPosition;
            float progress01 = 1 - (ttl / ttlMax);

            // move toward center reticle
            pos.x = Mathf.Lerp(0, maxDistance, 1 - progress01);
            if (isLeft) pos.x = -Mathf.Abs(pos.x);
            pos.y = 0;
            transform.localPosition = pos;

            // become more opaque as center reached
            SetColor(new Color(1, 1, 1, progress01));

            ttl -= Time.deltaTime;
            if (Mathf.Abs(pos.x) < 0.01f)
            {
                Destroy(gameObject);
            }
        }
    }

    void RotateIfLeft()
    {
        if (isLeft)
            transform.rotation = Quaternion.Euler(0, 0, 180f);
    }

    public void StartIndicator(float ttlMax, float maxDistance, bool isLeft)
    {

        this.ttlMax = ttl = ttlMax;
        this.maxDistance = maxDistance;
        this.isLeft = isLeft;
        RotateIfLeft();
        isStarted = true;
    }

    void SetColor(Color newColor)
    {
        foreach (Image img in images)
        {
            img.color = newColor;
        }
    }
}
