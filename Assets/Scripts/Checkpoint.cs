using UnityEngine;

[RequireComponent (typeof(Collider))]

public class Checkpoint : MonoBehaviour
{
    public int checkpointIndex;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerController player = other.GetComponent<PlayerController>();
            RaceManager.instance.CheckpointReached(checkpointIndex, player.playerNum);
            other.GetComponent<PlayerController>().currentCheckpoint = this;
        }
    }
}
