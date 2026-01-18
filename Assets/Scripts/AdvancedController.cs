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

        if (moveInput > 0)
        {
            currentSpeed += moveInput * acceleration * Time.deltaTime;
        }
        else if (moveInput < 0)
        {
            currentSpeed += moveInput * (acceleration / 2) * Time.deltaTime;
        }
        else
        {
            currentSpeed = Mathf.Lerp(currentSpeed, 0, Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.Space))
        {
            currentSpeed = Mathf.MoveTowards(currentSpeed, 0, brakeForce * Time.deltaTime);
        }

        currentSpeed = Mathf.Clamp(currentSpeed, -maxSpeed / 4, maxSpeed);
        transform.Translate(Vector3.forward * (currentSpeed / 10f) * Time.deltaTime);

        if (currentSpeed != 0)
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