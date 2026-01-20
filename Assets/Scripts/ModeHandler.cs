using UnityEngine;

public class ModeHandler : MonoBehaviour
{
    [Header("Mode Containers")]
    public GameObject raceContainer;
    public GameObject zoneContainer;

    void Start()
    {
        // Get the mode we saved in the menu
        string mode = PlayerPrefs.GetString("ActiveMode", "Race");

        if (mode == "Race")
        {
            raceContainer.SetActive(true);
            zoneContainer.SetActive(false);
        }
        else if (mode == "Time") // Matches your "Time" string in the Inspector
        {
            raceContainer.SetActive(false);
            zoneContainer.SetActive(true);
        }
    }
}