using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ControladorUi : MonoBehaviour
{
    public Text clipCountText, timeCountText, maletinCountText, lapCountText, arma, positionText;

    private int minutes, seconds = 0;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("IncreaseTime");
        Time.timeScale = 1; //Just making sure that the timeScale is right
    }

    // Update is called once per frame
    void Update()
    {
        UpdateTimeCountText();
    }

    public void UpdateObjectCount(string objectType, int numberOfObjectsCollected)
    {
        string newText = $"{numberOfObjectsCollected}";

        switch (objectType)
        {
            case "Clip":
                this.clipCountText.text = newText;

                break;
            case "Maletin":
                this.maletinCountText.text = $"{numberOfObjectsCollected}";

                break;
            default:
                break;
        }
    }

    public void UpdateLapCounter(int currentLapNumber, int raceLapNumber)
    {
        int currentLap = currentLapNumber == 0 ? 1 : currentLapNumber;

        this.lapCountText.text = $"{currentLap}/{raceLapNumber}";
    }

    public void UpdatePlayerPositionInRace(int playerPosition, int totalPlayers)
    {
        this.positionText.text = $"{playerPosition}/{totalPlayers}";
    }

    public void EndGame()
    {
        CurrentLevelController.CurrentLevelData = new LevelData("admin", 0, 0, -1);

        SceneManager.LoadScene(SceneNames.EndOfLevel);
    }

    private void UpdateTimeCountText()
    {
        timeCountText.text = $"{this.minutes.ToString().PadLeft(2, '0')}:{this.seconds.ToString().PadLeft(2, '0')}";
    }

    //Simple Coroutine
    IEnumerator IncreaseTime()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            this.seconds++;

            if (seconds % 60 == 0)
            {
                this.minutes++;
                this.seconds = 0;
            }
        }
    }
}