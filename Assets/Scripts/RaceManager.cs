using UnityEngine;

public class RaceManager : MonoBehaviour
{
    public static RaceManager instance;

    // Reference the whole screens, not just TMP_Text
    public GameObject winScreen;
    public GameObject loseScreen;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        // Make sure the screens are off at start
        if (winScreen != null)
            winScreen.SetActive(false);
        if (loseScreen != null)
            loseScreen.SetActive(false);
    }

    void Update()
    {
        // For testing
        if (Input.GetKeyDown(KeyCode.T))
        {
            PlayerWon();
        }
        if (Input.GetKeyDown(KeyCode.Y))
        {
            AIWon();
        }
    }

    public void PlayerWon()
    {
        Debug.Log("PLAYER WON");
        if (winScreen != null)
            winScreen.SetActive(true);

        EndRace();
    }

    public void AIWon()
    {
        Debug.Log("AI WON");
        if (loseScreen != null)
            loseScreen.SetActive(true);

        EndRace();
    }

    void EndRace()
    {
        // Stop the game
        Time.timeScale = 0f;
    }
}
