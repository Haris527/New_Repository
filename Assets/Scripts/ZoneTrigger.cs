using UnityEngine;

public class ZoneTrigger : MonoBehaviour
{
    [Header("Effects")]
    public GameObject sparkleEffect; // Drag your ZoneBurst Prefab here

    private void OnTriggerEnter(Collider other)
    {
        // 1. Check if the object hitting the zone is the Player
        if (other.CompareTag("Player"))
        {
            // 2. Spawn Particles (Realistic Vibe)
            if (sparkleEffect != null)
            {
                Instantiate(sparkleEffect, transform.position, Quaternion.identity);
            }

            // 3. Update the GameManager/Score
            GameManager manager = FindFirstObjectByType<GameManager>();
            if (manager != null)
            {
                manager.OnZoneHit();
            }

            // 4. Make the Cube disappear
            gameObject.SetActive(false);
        }
    }
}