using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;
using UnityEngine.SceneManagement;

public class GameMenuManager : MonoBehaviour
{
    [System.Serializable]
    public struct MapData
    {
        public string sceneName; // Must match "Green", "Snow", etc.
        public Sprite mapImage;  // The screenshot/icon of the map
    }

    [Header("Screens")]
    public GameObject homePage;
    public GameObject modeScreen;
    public GameObject mapScreen;
    public GameObject loadingScreen;

    [Header("Selection Display")]
    public TextMeshProUGUI modeDisplay;
    public Image mapPreviewDisplay; // Drag your new 'MapPreviewDisplay' here
    public MapData[] allMaps;       // Array to hold your map names and images

    private string selectedMap = "";
    private string selectedMode = "";

    void Start()
    {
        homePage.SetActive(true);
        modeScreen.SetActive(false);
        mapScreen.SetActive(false);
        loadingScreen.SetActive(false);
        UpdateSelectionUI();
    }

    public void SelectMode(string name)
    {
        selectedMode = name;
        PlayerPrefs.SetString("ActiveMode", name);
        UpdateSelectionUI();
        ReturnToHome();
    }

    public void SelectMap(string name)
    {
        selectedMap = name;
        UpdateSelectionUI();
        ReturnToHome();
    }

    void UpdateSelectionUI()
    {
        // Update Mode Text
        if (modeDisplay != null)
            modeDisplay.text = "" + (string.IsNullOrEmpty(selectedMode) ? "None" : selectedMode);

        // Update Map Image
        if (mapPreviewDisplay != null)
        {
            if (string.IsNullOrEmpty(selectedMap))
            {
                mapPreviewDisplay.gameObject.SetActive(false); // Hide if nothing selected
            }
            else
            {
                mapPreviewDisplay.gameObject.SetActive(true);
                // Find the sprite that matches the selected map name
                foreach (var map in allMaps)
                {
                    if (map.sceneName == selectedMap)
                    {
                        mapPreviewDisplay.sprite = map.mapImage;
                        break;
                    }
                }
            }
        }
    }

    // Navigation Functions
    public void ShowModeScreen() { homePage.SetActive(false); modeScreen.SetActive(true); }
    public void ShowMapScreen() { homePage.SetActive(false); mapScreen.SetActive(true); }
    void ReturnToHome() { modeScreen.SetActive(false); mapScreen.SetActive(false); homePage.SetActive(true); }

    public void TapToStart()
    {
        if (!string.IsNullOrEmpty(selectedMap) && !string.IsNullOrEmpty(selectedMode))
            StartCoroutine(StartLoading());
    }

    IEnumerator StartLoading()
    {
        homePage.SetActive(false);
        loadingScreen.SetActive(true);
        float progress = 0;
        while (progress < 1f)
        {
            progress += Time.deltaTime * 0.5f;
            yield return null;
        }
        SceneManager.LoadScene(selectedMap);
    }
}