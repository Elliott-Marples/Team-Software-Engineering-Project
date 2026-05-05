using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public GameObject sceneCamera;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    public void OnPlayerJoined()
    {
        sceneCamera.SetActive(false);
    }
}
