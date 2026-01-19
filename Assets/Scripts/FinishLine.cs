using UnityEngine;

public class FinishLine : MonoBehaviour
{
    private bool raceEnded = false;

  void OnTriggerEnter(Collider other)
{
    Debug.Log("Triggered by: " + other.name + " | Tag: " + other.tag);

    if (RaceManager.instance == null)
        Debug.LogError("RaceManager.instance is NULL");

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
