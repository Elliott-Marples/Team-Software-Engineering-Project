using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class RaceManager : MonoBehaviour
{
    public static RaceManager instance;

    [Header("UI References")]
    [SerializeField] private TextMeshProUGUI currentLapTimeText;
    [SerializeField] private TextMeshProUGUI bestLapTimeText;
    [SerializeField] private TextMeshProUGUI overallRaceTimeText;
    [SerializeField] private TextMeshProUGUI lapText;

    [Header("Race Settings")]
    [SerializeField] private Checkpoint[] checkpoints;
    [SerializeField] private int lastCheckpointIndex = -1;
    [SerializeField] private bool isCircuit = false;
    [SerializeField] private int totalLaps = 1;

    private int currentLap = 0;

    private bool raceStarted = false;
    private bool raceFinished = false;

    [Header("Lap Timer")]
    private float currentLapTime = 0f;
    private float overallRaceTime = 0f;
    private float bestLapTime = Mathf.Infinity;

    [SerializeField] public GameObject door;
    [SerializeField] public PlayerInputManager playerInputManager;
    private List<int> playersReady = new();

    #region Unity Functions

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (raceStarted)
        {
            UpdateTimers();
        }
        UpdateUI();
    }

    #endregion

    #region Checkpoint Management

    public void CheckpointReached(int checkpointIndex)
    {
        if ((!raceStarted && checkpointIndex != 0) || raceFinished) return;

        if(checkpointIndex == lastCheckpointIndex + 1)
        {
            UpdateCheckpoint(checkpointIndex);
        }
    }

    private void UpdateCheckpoint(int checkpointIndex)
    {
        if (checkpointIndex == 0)
        {
            if (!raceStarted)
            {
                StartRace();
            }
            else if (isCircuit && lastCheckpointIndex == checkpoints.Length - 1)
            {
                OnLapFinish();
            }
        }
        else if (!isCircuit && checkpointIndex == checkpoints.Length - 1)
        {
            OnLapFinish();
        }

        lastCheckpointIndex = checkpointIndex;
    }

    #endregion

    #region Race Management

    private void OnLapFinish()
    {
        currentLap++;

        if(currentLapTime < bestLapTime)
        {
            bestLapTime = currentLapTime;
        }

        if (currentLap >= totalLaps)
        {
            EndRace();
        }
        else
        {
            currentLapTime = 0f;
            lastCheckpointIndex = isCircuit ? 0 : -1;
        }

    }

    public void StartRace(int playerNum = -1)
    {
        // Add players to ready list if not already in there
        if (playerNum != -1 && !playersReady.Contains(playerNum))
        {
            playersReady.Add(playerNum);
        }

        // When all players have pressed start race button, start the race
        if (playerInputManager.playerCount == playersReady.Count)
        {
            raceStarted = true;
            raceFinished = false;

            Destroy(door);
            playerInputManager.DisableJoining();
        }

    }

    public void EndRace()
    {
        raceFinished = true;
        raceStarted = false;
    }

    private void UpdateTimers()
    {
        currentLapTime += Time.deltaTime;
        overallRaceTime += Time.deltaTime;
    }

    private void UpdateUI()
    {
        currentLapTimeText.text = "Current Time: " + FormatTime(currentLapTime);
        overallRaceTimeText.text = "Overall Time: " + FormatTime(overallRaceTime);
        lapText.text = "Lap: " + currentLap + "/" + totalLaps;
        bestLapTimeText.text = "Personal Best: " + FormatTime(bestLapTime);
    }


    #endregion

    #region Utility Functions

    private string FormatTime(float time)
    {
        if (float.IsInfinity(time) || time < 0) return "--:--";

        int minutes = (int)time / 60;
        float seconds = time % 60;
        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }
    #endregion
}
