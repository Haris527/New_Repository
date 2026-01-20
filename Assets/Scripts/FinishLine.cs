using UnityEngine;

public class FinishLine : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Find the GameManager in the scene and tell it we hit the line
            GameManager gm = FindFirstObjectByType<GameManager>();
            if (gm != null)
            {
                gm.OnFinishLineHit();
            }
        }
    }
}