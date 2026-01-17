using UnityEngine;
using UnityEngine.UI;

public class RaceManager : MonoBehaviour
{
    public static RaceManager instance;

    public GameObject winText;
    public GameObject loseText;

    void Awake()
    {
        instance = this;
        winText.SetActive(false);
        loseText.SetActive(false);
    }

    public void PlayerWon()
    {
        winText.SetActive(true);
        EndRace();
    }

    public void AIWon()
    {
        loseText.SetActive(true);
        EndRace();
    }

    void EndRace()
    {
        Time.timeScale = 0f; // pause game
    }
}
