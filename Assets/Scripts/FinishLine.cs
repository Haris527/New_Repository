using UnityEngine;

public class FinishLine : MonoBehaviour
{
    private bool raceEnded = false;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Triggered by: " + other.name);

        if (raceEnded)
            return;

        if (other.CompareTag("Player"))
        {
            raceEnded = true;
            RaceManager.instance.PlayerWon();
        }
        else if (other.CompareTag("AI"))
        {
            raceEnded = true;
            RaceManager.instance.PlayerLost();
        }
    }
}
