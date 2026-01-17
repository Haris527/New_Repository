using UnityEngine;

public class FinishLine : MonoBehaviour
{
    private bool raceEnded = false;

    void OnTriggerEnter(Collider other)
    {
        if (raceEnded) return;

        if (other.CompareTag("Player"))
        {
            raceEnded = true;
            RaceManager.instance.PlayerWon();
        }
        else if (other.CompareTag("AI"))
        {
            raceEnded = true;
            RaceManager.instance.AIWon();
        }
    }
}
