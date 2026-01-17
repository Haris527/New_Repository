using UnityEngine;

public enum TriggerType { StartLine, FinishLine }

public class LapTrigger : MonoBehaviour
{
    public TriggerType type;

    private void OnTriggerEnter(Collider other)
    {
        // Only trigger if the object hitting us is the Player car
        if (other.CompareTag("Player"))
        {
            GameManager manager = FindFirstObjectByType<GameManager>();

            if (type == TriggerType.StartLine)
            {
                manager.OnStartLineHit();
            }
            else if (type == TriggerType.FinishLine)
            {
                manager.OnFinishLineHit();
            }
        }
    }
}