using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] Button playBtn;

    private void Start()
    {
        playBtn.onClick.AddListener(() => SceneManager.LoadScene("Level"));
    }
}
