using UnityEngine;

public class RaceManager : MonoBehaviour
{
    // Singleton instance
    public static RaceManager instance;

    [Header("UI Screens")]
    public GameObject winScreen;
    public GameObject loseScreen;

    [Header("Timer")]
    public RaceTimer raceTimer;   // ✅ ADDED

    private bool raceEnded = false;

    private void Awake()
    {
        // Setup singleton
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        // Debug: Press T to simulate win
        if (Input.GetKeyDown(KeyCode.T))
        {
            PlayerWon();
        }
    }

    // Call this when player crosses finish line
    public void PlayerWon()
    {
        if (raceEnded) return;

        raceEnded = true;

        if (raceTimer != null)
            raceTimer.StopTimer();   // ✅ ADDED

        Debug.Log("Player Won!");
        if (winScreen != null)
            winScreen.SetActive(true);
    }

    // Call this if AI wins or player loses
    public void PlayerLost()
    {
        if (raceEnded) return;

        raceEnded = true;

        if (raceTimer != null)
            raceTimer.StopTimer();   // ✅ ADDED

        Debug.Log("Player Lost!");
        if (loseScreen != null)
            loseScreen.SetActive(true);
    }

    // Optional: Reset race
    public void ResetRace()
    {
        raceEnded = false;
        if (winScreen != null) winScreen.SetActive(false);
        if (loseScreen != null) loseScreen.SetActive(false);
    }
}
