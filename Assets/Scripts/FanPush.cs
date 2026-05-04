using UnityEngine;

public class FanPush : MonoBehaviour
{
    [SerializeField] private Vector3 direction;
    [SerializeField] private float strength;
    private GameObject currentTarget;
    private PlayerController currentTargetController;

    private void Awake()
    {
        direction.Normalize();
        direction *= strength;
    }

    private void OnTriggerEnter(Collider other)
    {
        currentTarget = other.gameObject;
        if (currentTarget.CompareTag("Player"))
        {
            currentTargetController = currentTarget.GetComponent<PlayerController>();
            currentTargetController.EnterFanArea(direction);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerController>().ExitFanArea();
        }
    }
}
