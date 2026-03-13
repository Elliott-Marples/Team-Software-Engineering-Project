using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    [Header("Spawn Setup")]
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private Transform[] spawnPoints;

    private void Start()
    {
        int playerCount = Mathf.Clamp(GameSettings.PlayerCount, 1, 4);

        for (int i = 0; i < playerCount; i++)
        {
            Transform spawnPoint = spawnPoints[i];
            GameObject playerInstance = Instantiate(playerPrefab, spawnPoint.position, spawnPoint.rotation);

            playerInstance.name = "Player_" + (i + 1);

            UnityEngine.Camera playerCamera = playerInstance.GetComponentInChildren<UnityEngine.Camera>();

            if (playerCamera != null)
            {
                playerCamera.rect = GetVerticalSplitRect(i, playerCount);

                AudioListener listener = playerCamera.GetComponent<AudioListener>();
                if (listener != null)
                {
                    listener.enabled = (i == 0);
                }
            }
        }
    }

    private Rect GetVerticalSplitRect(int playerIndex, int totalPlayers)
    {
        if (totalPlayers <= 1)
        {
            return new Rect(0f, 0f, 1f, 1f);
        }

        float width = 1f / totalPlayers;
        float x = playerIndex * width;

        return new Rect(x, 0f, width, 1f);
    }
}