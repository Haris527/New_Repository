using UnityEngine;

public class MinimapFollow : MonoBehaviour
{
    [Header("References")]
    public Transform playerCar; // Drag your 'car' object here

    [Header("Settings")]
    public bool rotateWithPlayer = true;

    void LateUpdate()
    {
        if (playerCar == null) return;

        // Keep the map following the car's position (if it's a 3D camera map)
        // transform.position = new Vector3(playerCar.position.x, transform.position.y, playerCar.position.z);

        if (rotateWithPlayer)
        {
            // Get the car's Y rotation
            Vector3 playerRotation = playerCar.eulerAngles;

            // Rotate the UI map in the opposite direction to keep 'Forward' facing up
            // We use 'z' because UI elements rotate on the Z-axis
            transform.localRotation = Quaternion.Euler(0, 0, playerRotation.y);
        }
    }
}