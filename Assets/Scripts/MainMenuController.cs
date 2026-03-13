using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [Header("Panels")]
    [SerializeField] public GameObject playPanel;
    [SerializeField] public GameObject playerCountPanel;

    [Header("Scene")]
    [SerializeField] private string gameSceneName = "GameScene";

    private void Start()
    {
        ShowPlayPanel();
    }

    public void ShowPlayPanel()
    {
        if (playPanel != null)
            playPanel.SetActive(true);

        if (playerCountPanel != null)
            playerCountPanel.SetActive(false);
    }

    public void ShowPlayerCountPanel()
    {
        if (playPanel != null)
            playPanel.SetActive(false);

        if (playerCountPanel != null)
            playerCountPanel.SetActive(true);
    }

    public void StartGame(int playerCount)
    {
        GameSettings.PlayerCount = Mathf.Clamp(playerCount, 1, 4);
        SceneManager.LoadScene(gameSceneName);
    }
}