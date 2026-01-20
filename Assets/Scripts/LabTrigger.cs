using UnityEngine;

public enum TriggerType { StartLine, FinishLine }

public class LapTrigger : MonoBehaviour
{
    public TriggerType type;
    private bool triggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (triggered) return;
        if (type != TriggerType.FinishLine) return;

        if (other.CompareTag("Player"))
        {
            triggered = true;
            RaceManager.instance.PlayerWon();
        }
        else if (other.CompareTag("AI"))
        {
            triggered = true;
            RaceManager.instance.PlayerLost();
        }
    }
}
