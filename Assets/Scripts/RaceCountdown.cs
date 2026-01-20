using UnityEngine;
using TMPro;
using System.Collections;

public class RaceCountdown : MonoBehaviour
{
    [Header("Countdown Settings")]
    public float countdownTime = 3f;
    public TMP_Text countdownText; // shared TMP_Text for countdown and race timer

    [Header("Cars")]
    public GameObject playerCar;
    public GameObject aiCar;

    [Header("Race Timer")]
    public RaceTimer raceTimer; // reference to RaceTimer script

    private bool raceStarted = false;

    void Start()
    {
        // Freeze cars at start
        FreezeCar(playerCar, true);
        FreezeCar(aiCar, true);

        // Start countdown
        StartCoroutine(CountdownCoroutine());
    }

    IEnumerator CountdownCoroutine()
    {
        float timer = countdownTime;

        while (timer > 0)
        {
            if (countdownText != null)
                countdownText.text = Mathf.Ceil(timer).ToString();

            yield return new WaitForSeconds(1f);
            timer -= 1f;
        }

        // Countdown finished
        if (countdownText != null)
            countdownText.text = "GO!";

        raceStarted = true;

        // Unfreeze cars
        FreezeCar(playerCar, false);
        FreezeCar(aiCar, false);

        // Start race timer
        if (raceTimer != null)
            raceTimer.StartTimer();

        // Wait 1 second then clear countdown text (keep TMP_Text active for race timer)
        if (countdownText != null)
            yield return new WaitForSeconds(1f);

        if (countdownText != null)
            countdownText.text = ""; // ready for race timer
    }

    void FreezeCar(GameObject car, bool freeze)
    {
        if (car == null) return;

        Rigidbody rb = car.GetComponent<Rigidbody>();
        if (rb != null)
            rb.isKinematic = freeze;

        var playerController = car.GetComponent<AdvancedCarController>();
        if (playerController != null)
            playerController.enabled = !freeze;

        var ai = car.GetComponent<AICarController>();
        if (ai != null)
            ai.enabled = !freeze;
    }

    public bool RaceStarted() => raceStarted;
}
