using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CommonUIController : MonoBehaviour
{
    [SerializeField]
    private Text clipCountText, timeCountText, briefcaseCountText, lapCountText, positionText;

    private int minutes, seconds = 0;

    /// <summary> Called before the first frame update. </summary>
    private void Start()
    {
        StartCoroutine("StartShowingClock");

        // Just making sure that the timeScale is right.
        Time.timeScale = 1;
    }

    /// <summary> Called once per frame. </summary>
    private void Update()
    {
        UpdateTimeCountText();
    }

    /// <summary>
    /// Updates the representation of the collected objects of the given type, with the given number
    /// of collected items.
    /// </summary>
    /// <param name="objectType"> The type of the object to update its representation. </param>
    /// <param name="numberOfObjectsCollected">
    /// The number of collected objects of the given type.
    /// </param>
    public void UpdateObjectCount(string objectType, int numberOfObjectsCollected)
    {
        string newText = $"{numberOfObjectsCollected}";

        switch (objectType)
        {
            case "Clip":
                this.clipCountText.text = newText;

                break;
            case "Briefcase":
                this.briefcaseCountText.text = $"{numberOfObjectsCollected}";

                break;
            default:
                break;
        }
    }

    /// <summary>
    /// Updates the representation of the laps in this race, according to the given current lap
    /// number and the race total laps.
    /// </summary>
    /// <param name="currentLapNumber"> The current lap number. </param>
    /// <param name="raceLapNumber"> The total of laps in the current race. </param>
    public void UpdateLapCounter(int currentLapNumber, int raceLapNumber)
    {
        int currentLap = currentLapNumber == 0 ? 1 : currentLapNumber;

        this.lapCountText.text = $"{currentLap}/{raceLapNumber}";
    }

    /// <summary>
    /// Updates the representation of the player position in the current race, according to the
    /// player position and the total of players.
    /// </summary>
    /// <param name="playerPosition"> The current player position. </param>
    /// <param name="totalPlayers"> The total players number in the current race. </param>
    public void UpdatePlayerPositionInRace(int playerPosition, int totalPlayers)
    {
        this.positionText.text = $"{playerPosition}/{totalPlayers}";
    }

    /// <summary> Ends the game due to the boss. </summary>
    public void GameOver()
    {
        CurrentLevelController.CurrentLevelData = new LevelData("admin", 0, 0, -1);

        SceneManager.LoadScene(SceneNames.EndOfLevel);
    }

    private void UpdateTimeCountText()
    {
        timeCountText.text = $"{this.minutes.ToString().PadLeft(2, '0')}:{this.seconds.ToString().PadLeft(2, '0')}";
    }

    /// <summary> Starts showing the clock. </summary>
    /// <returns>
    /// An empty <see cref="IEnumerator"/> that enables this method to be called as a Coroutine.
    /// </returns>
    private IEnumerator StartShowingClock()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);

            if (++this.seconds % 60 == 0)
            {
                this.minutes++;
                this.seconds = 0;
            }
        }
    }
}