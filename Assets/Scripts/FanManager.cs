using System.Collections;
using UnityEngine;

public class FanManager : MonoBehaviour
{
    private float rotationSpeed = 0f;

    void Start()
    {
        StartCoroutine(FanRoutine());
    }

    void Update()
    {
        // Always rotate using current speed
        transform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime);
    }

    IEnumerator FanRoutine()
    {
        while (true)
        {
            // Start fan
            rotationSpeed = 2000f;
            yield return new WaitForSeconds(5f);

            // Stop fan
            rotationSpeed = 0f;
            yield return new WaitForSeconds(5f);
        }
    }
}