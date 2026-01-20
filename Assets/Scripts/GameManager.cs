using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement; // Required for changing scenes

public class GameManager : MonoBehaviour
{
    public enum GameMode { Race, ZoneHit }
    public GameMode selectedMode;

    [Header("Containers")]
    public GameObject raceContainer;
    public GameObject zoneHitContainer;

    [Header("UI References")]
    public TextMeshProUGUI statusText;
    public TextMeshProUGUI timerText;
    public GameObject winPanel;
    public GameObject losePanel;

    [Header("Game Settings")]
    public float zoneModeTimeLimit = 45f; // Set to 45 in Inspector

    [Header("Game State")]
    private int zonesCollected = 0;
    private int totalZones;
    private float currentTime; // Used for both counting up (Race) and down (Zone)
    private bool isRacing = false;
    private bool allZonesCollected = false;
    private bool isGameActive = true;

    [Header("UI Feedback")]
    public Color startColor = Color.green;
    public Color warningColor = Color.yellow;
    public Color dangerColor = Color.red;

    void Start()
    {
        // Count zones automatically
        totalZones = zoneHitContainer.GetComponentsInChildren<ZoneTrigger>(true).Length;

        string savedMode = PlayerPrefs.GetString("ActiveMode", "Race");

        if (savedMode == "Race")
        {
            selectedMode = GameMode.Race;
            raceContainer.SetActive(true);
            zoneHitContainer.SetActive(false);
            currentTime = 0f; // Race starts at 0
        }
        else
        {
            selectedMode = GameMode.ZoneHit;
            raceContainer.SetActive(false);
            zoneHitContainer.SetActive(true);
            currentTime = zoneModeTimeLimit; // Zone starts at 45
            UpdateZoneUI();
        }

        if (winPanel != null) winPanel.SetActive(false);
        if (losePanel != null) losePanel.SetActive(false);

        if (timerText != null)
        {
            timerText.text = "" + currentTime.ToString("F2") + "s";
            // Set the initial color based on mode
            timerText.color = (selectedMode == GameMode.Race) ? Color.white : startColor;
        }
    }

    public void PlayAgain()
    {
        // Reloads the current active scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ExitToHome()
    {
        // Loads your main menu scene. Ensure the name matches exactly
        SceneManager.LoadScene("UI");
    }

    void FinishGame(bool won)
    {
        isGameActive = false;
        isRacing = false;

        // 1. Stop the Player's car movement
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            var controller = player.GetComponent<AdvancedCarController>();
            if (controller != null) controller.enabled = false;
        }

        // 2. Stop all AI cars in the scene
        AICarController[] ais = Object.FindObjectsByType<AICarController>(FindObjectsSortMode.None);
        foreach (AICarController ai in ais)
        {
            ai.StopCar(); // Ensure you added the StopCar() function to AICarController
        }

        // 3. Show the appropriate UI panel
        if (won)
        {
            if (winPanel != null) winPanel.SetActive(true);
        }
        else
        {
            if (losePanel != null) losePanel.SetActive(true);
        }
    }

    void Update()
    {
        if (!isGameActive || !isRacing) return;

        if (selectedMode == GameMode.Race)
        {
            currentTime += Time.deltaTime;
            timerText.color = Color.white; // Default color for Race mode
        }
        else
        {
            currentTime -= Time.deltaTime;

            // --- COLOR LOGIC ---
            if (currentTime > 20)
            {
                timerText.color = startColor;
            }
            else if (currentTime <= 20 && currentTime > 10)
            {
                timerText.color = warningColor;
            }
            else if (currentTime <= 10)
            {
                timerText.color = dangerColor;

                // --- BLINK LOGIC (Every 1 second) ---
                // We use Sine wave or Floor to toggle visibility
                bool isBlinkVisible = Mathf.FloorToInt(currentTime * 2) % 2 == 0;
                timerText.enabled = isBlinkVisible;
            }

            // FAIL condition: Time runs out
            if (currentTime <= 0)
            {
                currentTime = 0;
                timerText.enabled = true; // Ensure text is visible when failing
                FinishGame(false);
            }
        }

        if (timerText != null)
            timerText.text = "" + currentTime.ToString("F2") + "s";
    }

    public void OnZoneHit()
    {
        if (selectedMode != GameMode.ZoneHit) return;

        zonesCollected++;
        UpdateZoneUI();

        if (zonesCollected >= totalZones)
        {
            allZonesCollected = true;
            if (statusText != null) statusText.text = "";
        }
    }

    void UpdateZoneUI()
    {
        if (statusText != null) statusText.text = $"Zones: {zonesCollected}/{totalZones}";
    }

    public void OnStartLineHit()
    {
        if (!isRacing) isRacing = true;
    }

    public void OnFinishLineHit()
    {
        if (selectedMode == GameMode.ZoneHit)
        {
            // Win only if zones are done and time is left
            if (allZonesCollected && currentTime > 0)
            {
                FinishGame(true);
            }
            else
            {
                FinishGame(false);
            }
        }
        else
        {
            FinishGame(true);
        }
    }

   

   
}