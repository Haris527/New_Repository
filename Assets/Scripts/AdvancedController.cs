using UnityEngine;
using TMPro;

public class AdvancedCarController : MonoBehaviour
{
    public float acceleration = 10f;      // ⬅ Reduced
    public float maxSpeed = 200f;          // ⬅ New top speed
    public float brakeForce = 30f;
    public float steeringSpeed = 100f;
    public float currentSpeed = 0f;

    [Header("Steering Control")]
    public float steeringSensitivityMultiplier = 0.6f; // ⬅ Lower = smoother turns

    [Header("UI References")]
    public RectTransform speedNeedle;
    public TextMeshProUGUI gearText;
    public TextMeshProUGUI speedValueText;

    [Header("Needle Settings")]
    public float startRotation = 135f;
    public float endRotation = -135f;

    [Header("UI Speed Control")]
    public float uiSmoothSpeed = 2f;

    private float displayedSpeed = 0f;

    void Update()
    {
        HandleMovement();
        UpdateUI();
    }

    void HandleMovement()
    {
        float moveInput = Input.GetAxis("Vertical");
        float turnInput = Input.GetAxis("Horizontal");

        // BRAKING
        if (Input.GetKey(KeyCode.Space))
        {
            currentSpeed = Mathf.MoveTowards(currentSpeed, 0, brakeForce * Time.deltaTime);
        }
        // ACCELERATION
        else if (Mathf.Abs(moveInput) > 0.1f)
        {
            if (moveInput > 0)
                currentSpeed += moveInput * acceleration * Time.deltaTime;
            else
                currentSpeed += moveInput * (acceleration / 2f) * Time.deltaTime;
        }
        // COASTING
        else
        {
            currentSpeed = Mathf.Lerp(currentSpeed, 0, Time.deltaTime);
        }

        // CLAMP SPEED
        currentSpeed = Mathf.Clamp(currentSpeed, -maxSpeed / 4f, maxSpeed);

        // APPLY MOVEMENT (UNCHANGED)
        transform.Translate(Vector3.forward * (currentSpeed / 10f) * Time.deltaTime);

        // STEERING (REDUCED SENSITIVITY)
        if (Mathf.Abs(currentSpeed) > 0.1f)
        {
            float speedFactor = Mathf.Clamp01(Mathf.Abs(currentSpeed) / maxSpeed);
            float turnAmount =
                turnInput *
                steeringSpeed *
                steeringSensitivityMultiplier *
                speedFactor *
                Time.deltaTime;

            transform.Rotate(0, turnAmount, 0);
        }
    }

    void UpdateUI()
    {
        float targetSpeed = Mathf.Abs(currentSpeed);

        displayedSpeed = Mathf.Lerp(
            displayedSpeed,
            targetSpeed,
            Time.deltaTime * uiSmoothSpeed
        );

        if (speedNeedle != null)
        {
            float speedRatio = displayedSpeed / maxSpeed;
            float needleAngle = Mathf.Lerp(startRotation, endRotation, speedRatio);
            speedNeedle.localRotation = Quaternion.Euler(0, 0, needleAngle);
        }

        if (speedValueText != null)
        {
            speedValueText.text = Mathf.RoundToInt(displayedSpeed).ToString();
        }

        if (gearText != null)
        {
            int gear = (int)(displayedSpeed / 50f) + 1;
            gearText.text = " " + Mathf.Clamp(gear, 1, 5);
        }
    }
}
