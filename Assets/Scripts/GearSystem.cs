using UnityEngine;
using TMPro;

public class GearSystem : MonoBehaviour
{
    public Rigidbody carRigidbody;
    public TextMeshProUGUI gearText;

    void FixedUpdate()
    {
        float speed = carRigidbody.linearVelocity.magnitude * 3.6f;
        int gear = GetGear(speed);
        gearText.text = gear == 0 ? "N" : gear.ToString();
    }

    int GetGear(float speed)
    {
        if (speed < 1f) return 0;   // Neutral
        if (speed < 10f) return 1;
        if (speed < 25f) return 2;
        if (speed < 45f) return 3;
        if (speed < 70f) return 4;
        return 5;
    }
}
