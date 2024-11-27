using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour
{
    bool isPaused;
    [SerializeField] GameObject pausePanel;
    [SerializeField] Button menuBtn;
    [SerializeField] Button quitBtn;

    AudioLowPassFilter lowPassFilter;

    private void Start()
    {
        menuBtn.onClick.AddListener(() => SceneManager.LoadScene("MainMenu"));
        quitBtn.onClick.AddListener(() => GameManager.Quit());
        lowPassFilter = GameObject.FindGameObjectWithTag("MusicSource").GetComponent<AudioLowPassFilter>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
                EndPause(); 
            else
                StartPause();
        }
    }

    void StartPause()
    {
        isPaused = true;
        pausePanel.SetActive(true);
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        if (lowPassFilter) lowPassFilter.enabled = true;
    }

    void EndPause()
    {
        isPaused = false;
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        if (lowPassFilter) lowPassFilter.enabled = false;
    }
}
