using UnityEngine;
using TMPro;

public class Speedometer : MonoBehaviour
{
    public AdvancedCarController car; // reference your car script
    public TextMeshProUGUI speedText;
    public RectTransform needle;

    public float maxSpeed = 250f;
    public float minRotation = 135f;
    public float maxRotation = -135f;

    void Update()
    {
        if (car == null) return;

        float speed = Mathf.Abs(car.currentSpeed);

        // Speed number
        speedText.text = Mathf.RoundToInt(speed).ToString();

        // Needle
        float t = Mathf.Clamp01(speed / maxSpeed);
        float rotation = Mathf.Lerp(minRotation, maxRotation, t);
        needle.localRotation = Quaternion.Euler(0, 0, rotation);
    }
}
