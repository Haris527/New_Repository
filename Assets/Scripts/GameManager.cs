using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public enum GameMode { Race, ZoneHit }
    public GameMode selectedMode;

    [Header("Containers")]
    public GameObject raceContainer;
    public GameObject zoneHitContainer;

    [Header("UI References")]
    public TextMeshProUGUI statusText;
    public GameObject resultPanel; // A UI panel that pops up at the end

    private int zonesCollected = 0;
    private int totalZones;
    private float timer = 0f;
    private bool isGameActive = true;
    [Header("Race Logic")]
    private float raceTimer = 0f;
    private bool isRacing = false;
    public TextMeshProUGUI timerText;

    void Start()
    {
        // Count how many zones you placed inside the container automatically
        totalZones = zoneHitContainer.GetComponentsInChildren<ZoneTrigger>(true).Length;

        if (selectedMode == GameMode.Race)
        {
            raceContainer.SetActive(true);
            zoneHitContainer.SetActive(false);
        }
        else
        {
            raceContainer.SetActive(false);
            zoneHitContainer.SetActive(true);
        }
    }

    void Update()
    {
        if (isGameActive && selectedMode == GameMode.ZoneHit)
        {
            timer += Time.deltaTime;
            statusText.text = $"Zones: {zonesCollected}/{totalZones}\nTime: {timer:F2}s";
        }
        if (isRacing)
        {
            raceTimer += Time.deltaTime;
            if (timerText != null) timerText.text = "Time: " + raceTimer.ToString("F2") + "s";
        }
    }

    public void OnZoneHit()
    {
        zonesCollected++;
        if (zonesCollected >= totalZones)
        {
            FinishGame();
        }
    }

    void FinishGame()
    {
        isGameActive = false;
        Debug.Log($"Finished! Total Time: {timer:F2}s");
        if (resultPanel != null) resultPanel.SetActive(true);
        // You can add logic here to show "S Rank" if timer < 60, etc.
    }
    public void OnStartLineHit()
    {
        if (!isRacing)
        {
            Debug.Log("Race Started!");
            raceTimer = 0f;
            isRacing = true;
        }
    }

    public void OnFinishLineHit()
    {
        if (isRacing)
        {
            isRacing = false;
            Debug.Log("Race Finished! Final Time: " + raceTimer.ToString("F2"));
            // Show your result panel here!
        }
    }
}