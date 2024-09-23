using UnityEngine;

public class BeatCube : MonoBehaviour
{
    [SerializeField] BeatManager bm;
    [SerializeField] GameObject[] modeParents; // each index's children are 'activated' at a time

    int cMode = 1;
    GameObject cParent;

    void Start()
    {
        cParent = modeParents[modeParents.Length-1];
        foreach (GameObject obj in modeParents)
        {
            obj.SetActive(false);
        }
    }

    void OnEnable()
    {
        bm.OnBeat.AddListener(() =>
        {
            if (++cMode > modeParents.Length) cMode = 1;
            cParent.SetActive(false);
            cParent = modeParents[cMode-1];
            cParent.SetActive(true); // TODO: replace activation with custom event/function for other toggleable stuff
        });
    }
}
