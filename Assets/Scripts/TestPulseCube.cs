using UnityEngine;

// on music beats sets material to random color
public class TestPulseCube : MonoBehaviour
{

    [SerializeField] BeatManager bm;
    Material mat;

    private void OnEnable()
    {
        mat = GetComponent<MeshRenderer>().material;
        BeatManager.OnBeat.AddListener(() =>
        {
            float r = Random.Range(0, 100) / 100f;
            float g = Random.Range(0, 100) / 100f;
            float b = Random.Range(0, 100) / 100f;
            mat.color = new Vector4(r, g, b, 1);
        });
    }
}
