using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Player4DetectionTextScript : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI ConnectedText;
    public ControllerDetectionScript controllerScript;
    int myDeviceCount;
    public Image image;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Color color;
        if (ColorUtility.TryParseHtmlString("#999999", out color))
        {
            image.color = color;
        }
    }

    // Update is called once per frame
    public void updateplayerindicator1()
    {
        myDeviceCount = controllerScript.deviceCount;
        if (myDeviceCount >= 4)
        {
            ConnectedText.text = "Connected";
            if (ColorUtility.TryParseHtmlString("#87CEFA", out Color color))
            {
                image.color = color;
            }
        }
        else
        {
            ConnectedText.text = "";
            if (ColorUtility.TryParseHtmlString("#999999", out Color color))
            {
                image.color = color;
            }
        }
    }
}
