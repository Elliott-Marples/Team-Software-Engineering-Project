using UnityEngine;

[RequireComponent (typeof(Collider))]

public class Checkpoint : MonoBehaviour
{
    public int checkpointIndex;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            RaceManager.instance.CheckpointReached(checkpointIndex);
            other.GetComponent<PlayerController>().currentCheckpoint = this;
        }
    }
}
