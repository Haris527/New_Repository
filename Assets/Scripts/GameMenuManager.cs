using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;
using UnityEngine.SceneManagement; // Allows us to load the race tracks

public class GameMenuManager : MonoBehaviour
{
    [Header("Screens")]
    public GameObject homePage;
    public GameObject modeScreen;
    public GameObject mapScreen;
    public GameObject loadingScreen;

    [Header("UI Elements")]
    public Image loadingBarFill;
    public TextMeshProUGUI warningText;

    private string selectedMap = "";
    private string selectedMode = "";

    void Start()
    {
        // Set initial menu state
        homePage.SetActive(true);
        modeScreen.SetActive(false);
        mapScreen.SetActive(false);
        loadingScreen.SetActive(false);
        warningText.gameObject.SetActive(false);
    }

    // Functions for bottom navigation buttons
    public void ShowModeScreen() { homePage.SetActive(false); modeScreen.SetActive(true); }
    public void ShowMapScreen() { homePage.SetActive(false); mapScreen.SetActive(true); }

    // Functions for specific selection buttons (Desert, Snow, Green)
    public void SelectMap(string name) { selectedMap = name; ReturnToHome(); }
    public void SelectMode(string name) { selectedMode = name; ReturnToHome(); }

    void ReturnToHome()
    {
        modeScreen.SetActive(false);
        mapScreen.SetActive(false);
        homePage.SetActive(true);
    }

    public void TapToStart()
    {
        // Checks if both are selected before starting the loading bar
        if (string.IsNullOrEmpty(selectedMap) || string.IsNullOrEmpty(selectedMode))
        {
            warningText.text = "Please select both Map and Mode!";
            warningText.gameObject.SetActive(true);
            return;
        }
        StartCoroutine(StartLoading());
    }

    IEnumerator StartLoading()
    {
        homePage.SetActive(false);
        loadingScreen.SetActive(true);

        float progress = 0;
        while (progress < 1f)
        {
            progress += Time.deltaTime * 0.5f; // Controls loading speed
            loadingBarFill.fillAmount = progress;
            yield return null;
        }

        // Loads the level you picked using its name from the Scene List
        SceneManager.LoadScene(selectedMap);
    }
}