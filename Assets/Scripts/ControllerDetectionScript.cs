using UnityEngine;
using UnityEngine.InputSystem;

public class ControllerDetectionScript : MonoBehaviour
{
    public int deviceCount;

    public Player1DetectionTextScript player1UI;
    public Player2DetectionTextScript player2UI;
    public Player3DetectionTextScript player3UI;
    public Player4DetectionTextScript player4UI;

    void Start()
    {
        UpdateDeviceCount();
        InputSystem.onDeviceChange += OnDeviceChange;
    }

    void OnDestroy()
    {
        InputSystem.onDeviceChange -= OnDeviceChange;
    }

    void OnDeviceChange(InputDevice device, InputDeviceChange change)
    {
        if (change == InputDeviceChange.Added || change == InputDeviceChange.Removed)
        {
            UpdateDeviceCount();
        }
    }

    void UpdateDeviceCount()
    {
        deviceCount = 0;

        foreach (var device in InputSystem.devices)
        {
            if (device is Mouse)
                continue;

            deviceCount++;
        }

        Debug.Log("Devices (no mouse): " + deviceCount);

        if(player1UI != null)
        {
            player1UI.updateplayerindicator1();
        }
        if (player2UI != null)
        {
            player2UI.updateplayerindicator1();
        }
        if (player3UI != null)
        {
            player3UI.updateplayerindicator1();
        }
        if (player4UI != null)
        {
            player4UI.updateplayerindicator1();
        }
    }
}