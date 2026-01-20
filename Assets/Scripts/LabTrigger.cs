using UnityEngine;

public enum TriggerType { StartLine, FinishLine }

public class LapTrigger : MonoBehaviour
{
    public TriggerType type;
    private bool triggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (triggered) return;

        // Find managers
        GameManager gm = Object.FindFirstObjectByType<GameManager>();

        // 1. START LINE LOGIC (Player Only)
        if (type == TriggerType.StartLine && other.CompareTag("Player"))
        {
            triggered = true;
            if (gm != null) gm.OnStartLineHit();
            Debug.Log("Start Line Triggered!");
        }
        // 2. FINISH LINE LOGIC (Player or AI)
        else if (type == TriggerType.FinishLine)
        {
            if (other.CompareTag("Player"))
            {
                triggered = true;
                if (gm != null) gm.OnFinishLineHit();
                if (RaceManager.instance != null) RaceManager.instance.PlayerWon();
            }
            else if (other.CompareTag("AI"))
            {
                triggered = true;
                if (RaceManager.instance != null) RaceManager.instance.PlayerLost();
            }
        }
    }
}