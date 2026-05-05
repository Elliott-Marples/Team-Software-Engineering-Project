using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [Header("Panels")]
    [SerializeField] public GameObject mainPanel;
    [SerializeField] public GameObject settingsPanel;

    [Header("Scene")]
    [SerializeField] private string gameSceneName = "GameScene";

    public ControllerDetectionScript controllerScript;

    private void Start()
    {
        ShowMainPanel();
        mainPanel.SetActive(true);
        settingsPanel.SetActive(false);
    }

    public void ShowMainPanel()
    {
        if (mainPanel != null)
            mainPanel.SetActive(true);

        if (settingsPanel != null)
            settingsPanel.SetActive(false);
    }

    public void ShowSettingsPanel()
    {
        if (mainPanel != null)
            mainPanel.SetActive(false);

        if (settingsPanel != null)
            settingsPanel.SetActive(true);

    }

    public void StartGame()
    {
        SceneManager.LoadScene(gameSceneName);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}