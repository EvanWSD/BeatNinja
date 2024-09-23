using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Hitscan : MonoBehaviour
{
    float maxDist = 100f;
    [SerializeField] Image reticleImg;

    void Update()
    {
        PerformHitscan();
    }

    void PerformHitscan() {
        // Define a ray from the center of the camera's viewport (screen center)
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, maxDist))
        {
            if (hit.collider.gameObject.CompareTag("TargetButton")) {
                reticleImg.color = Color.red;
                if (Input.GetMouseButtonDown(0))
                {
                    Material mat = hit.collider.GetComponent<MeshRenderer>().material;
                    float r = Random.Range(0, 100f) / 100f;
                    float g = Random.Range(0, 100f) / 100f;
                    float b = Random.Range(0, 100f) / 100f;
                    mat.color = new Color(r, g, b); 
                }
            }
        }
        else
        {
            reticleImg.color = Color.white;
        }
    }
}
