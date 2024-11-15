using UnityEngine;
using System.Collections.Generic;
using System.Net.Security;

public class DashDetector : MonoBehaviour
{
    public float maxDistance = 50f;
    Camera cam;

    [SerializeField] bool showRangeGizmo;

    private void Start()
    {
        cam = Camera.main;
    }

    private void Update()
    {
        List<GameObject> dashTargets = GetObjectsInView();

        foreach (GameObject obj in dashTargets)
        {
            // detect
        }
    }

    List<GameObject> GetObjectsInView()
    {
        List<GameObject> objectsInView = new List<GameObject>();
        Plane[] frustumPlanes = GeometryUtility.CalculateFrustumPlanes(cam);

        Collider[] allObjects = Physics.OverlapSphere(cam.transform.position, maxDistance);

        foreach (Collider col in allObjects)
        {
            if (!col.CompareTag("Enemy")) continue;

            GameObject obj = col.gameObject;
            Vector3 directionToObj = obj.transform.position - cam.transform.position;

            // Check if the object is within the camera's frustum
            if (GeometryUtility.TestPlanesAABB(frustumPlanes, col.bounds))
            {
                // Optionally, check if the object is in front of the camera
                if (Vector3.Dot(cam.transform.forward, directionToObj) > 0)
                {
                    objectsInView.Add(obj);
                }
            }
        }

        return objectsInView;
    }

    private void OnDrawGizmos()
    {
        if (showRangeGizmo)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(Camera.main.transform.position, maxDistance);
        }
    }
}
