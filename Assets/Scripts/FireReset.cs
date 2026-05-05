using UnityEngine;

public class FireReset : MonoBehaviour
{
    private GameObject currentTarget;
    private PlayerController currentTargetController;

    private void OnTriggerEnter(Collider other)
    {
        currentTarget = other.gameObject;
        if (currentTarget.CompareTag("Player"))
        {
            currentTargetController.ResetPlayer(this.gameObject);
        }
    }
}
