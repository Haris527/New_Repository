using UnityEngine;

public class CarEngineSound : MonoBehaviour
{
    public AudioSource engineSource;

    [Header("Pitch Settings")]
    public float minPitch = 0.8f;
    public float maxPitch = 2.0f;

    void Update()
    {
        float throttle = Mathf.Abs(Input.GetAxis("Vertical"));

        if (throttle > 0.05f)
        {
            if (!engineSource.isPlaying)
                engineSource.Play();

            engineSource.pitch = Mathf.Lerp(minPitch, maxPitch, throttle);
        }
        else
        {
            // Idle engine sound
            if (!engineSource.isPlaying)
                engineSource.Play();

            engineSource.pitch = minPitch;
        }
    }
}
