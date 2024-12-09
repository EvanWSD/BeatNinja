using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuSettings : MonoBehaviour
{
    [SerializeField] Button playBtn;

    [SerializeField] TextMeshProUGUI godModeText;
    bool godModeSetting;

    void Start()
    {
        playBtn.onClick.AddListener(ApplySettings);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            godModeSetting = !godModeSetting;
            UpdateSettingsUI(godModeSetting);
        }
    }

    void ApplySettings()
    {
        GameManager.SetGodModeEnabled(godModeSetting);
    }

    void UpdateSettingsUI(bool b)
    {
        string setting = b ? "ON" : "OFF";
        godModeText.text = $"Godmode: {setting} [F]";
    }
}
