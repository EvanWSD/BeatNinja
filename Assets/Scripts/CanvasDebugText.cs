using TMPro;
using UnityEngine;

public class CanvasDebugText : MonoBehaviour
{
    static TextMeshProUGUI text;

    private void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    public static void Write(float n, string label="")
    {
        if (label != "")
            label += ": ";
        text.text = label + Round(n, 3).ToString();
    }

    public static float Round(float value, float numDecimalPoints)
    {
        float num = Mathf.Pow(10, numDecimalPoints);
        return Mathf.Round(value * num) / num;
    }
}
