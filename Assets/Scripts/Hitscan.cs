using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

// unused, might reuse for basic interactables eg elevator button

public class Hitscan : MonoBehaviour
{
    float maxDist = 100f;
    bool lookingAtShootable;
    [SerializeField] Image reticleImg;

    protected void PerformHitscan() {
        // Define a ray from the center of the camera's viewport (screen center)
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, maxDist))
        {
            if (hit.collider.GetComponent(typeof(MonoBehaviour)) is IShootable shootable) {
                lookingAtShootable = true;
                reticleImg.color = Color.red;
                if (Input.GetMouseButtonDown(0))
                {
                    shootable.OnShot.Invoke();
                }
            }
        }
        else
        {
            lookingAtShootable = false;
            reticleImg.color = Color.white;
        }
    }
}
