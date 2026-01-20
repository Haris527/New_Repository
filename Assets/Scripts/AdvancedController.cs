using UnityEngine;
using TMPro; // Needed for TextMeshPro

public class AdvancedCarController : MonoBehaviour
{
    public float acceleration = 15f;
    public float maxSpeed = 250f;
    public float brakeForce = 30f;
    public float steeringSpeed = 100f;
    public float currentSpeed = 0f;

    [Header("UI References")]
    public RectTransform speedNeedle;
    public TextMeshProUGUI gearText;
    public TextMeshProUGUI speedValueText; // NEW: The numbers (236 km/h)

    [Header("Needle Settings")]
    public float startRotation = 135f; // Adjust these to match your gauge
    public float endRotation = -135f;

    void Update()
    {
        HandleMovement();
        UpdateUI();
    }

    void HandleMovement()
    {
        float moveInput = Input.GetAxis("Vertical");
        float turnInput = Input.GetAxis("Horizontal");

        // 1. BRAKE CHECK: This must come first to override acceleration
        if (Input.GetKey(KeyCode.Space))
        {
            // Forcefully move speed to 0. Use a high 'brakeForce' in Inspector (e.g., 100)
            currentSpeed = Mathf.MoveTowards(currentSpeed, 0, brakeForce * Time.deltaTime);
        }
        // 2. ACCELERATION: Only runs if you ARE NOT braking
        else if (Mathf.Abs(moveInput) > 0.1f)
        {
            if (moveInput > 0)
            {
                currentSpeed += moveInput * acceleration * Time.deltaTime;
            }
            else if (moveInput < 0)
            {
                // Reversing logic
                currentSpeed += moveInput * (acceleration / 2) * Time.deltaTime;
            }
        }
        // 3. COASTING: Natural slow down when no keys are pressed
        else
        {
            currentSpeed = Mathf.Lerp(currentSpeed, 0, Time.deltaTime);
        }

        // 4. CLAMP SPEED: Keep within max speed limits
        currentSpeed = Mathf.Clamp(currentSpeed, -maxSpeed / 4f, maxSpeed);

        // 5. APPLY MOVEMENT
        transform.Translate(Vector3.forward * (currentSpeed / 10f) * Time.deltaTime);

        // 6. STEERING: Only rotates if moving
        if (Mathf.Abs(currentSpeed) > 0.1f)
        {
            float turnAmount = turnInput * steeringSpeed * Time.deltaTime * (Mathf.Abs(currentSpeed) / 50f);
            transform.Rotate(0, turnAmount, 0);
        }
    }

    void UpdateUI()
    {
        if (speedNeedle != null)
        {
            // Map speed to the rotation range
            float speedRatio = Mathf.Abs(currentSpeed) / maxSpeed;
            float needleAngle = Mathf.Lerp(startRotation, endRotation, speedRatio);
            speedNeedle.localRotation = Quaternion.Euler(0, 0, needleAngle);
        }

        if (speedValueText != null)
        {
            // Show the number (km/h)
            speedValueText.text = Mathf.RoundToInt(Mathf.Abs(currentSpeed)).ToString();
        }

        if (gearText != null)
        {
            int gear = (int)(Mathf.Abs(currentSpeed) / 50) + 1;
            gearText.text = " " + (gear > 5 ? 5 : gear);
        }
    }
}