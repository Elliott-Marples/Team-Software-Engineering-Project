using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [Header("Panels")]
    [SerializeField] public GameObject mainPanel;
    [SerializeField] public GameObject playerCountPanel;
    [SerializeField] public GameObject settingsPanel;

    [Header("Scene")]
    [SerializeField] private string gameSceneName = "GameScene";

    public ControllerDetectionScript controllerScript;

    private void Start()
    {
        ShowMainPanel();
        mainPanel.SetActive(true);
        playerCountPanel.SetActive(false);
        settingsPanel.SetActive(false);
    }

    public void ShowMainPanel()
    {
        if (mainPanel != null)
            mainPanel.SetActive(true);

        if (playerCountPanel != null)
            playerCountPanel.SetActive(false);

        if (settingsPanel != null)
            settingsPanel.SetActive(false);
    }

    public void ShowPlayerCountPanel()
    {
        if (mainPanel != null)
            mainPanel.SetActive(false);

        if (playerCountPanel != null)
            playerCountPanel.SetActive(true);

        if (settingsPanel != null)
            settingsPanel.SetActive(false);
    }

    public void ShowSettingsPanel()
    {
        if (mainPanel != null)
            mainPanel.SetActive(false);

        if (playerCountPanel != null)
            playerCountPanel.SetActive(false);

        if (settingsPanel != null)
            settingsPanel.SetActive(true);

    }

    public void StartGame()
    {
        GameSettings.PlayerCount = controllerScript.deviceCount;

        SceneManager.LoadScene(gameSceneName);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}