using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ControladorUi : MonoBehaviour
{
    public Text clipCountText, timeCountText, maletinCountText, lapCountText, arma, positionText;

    private int minutes, seconds = 0;

    private bool endWindowIsActive = false;

    private Rect mainWindowRect;

    void OnGUI()
    {
        if (endWindowIsActive)
        {
            this.mainWindowRect = this.CalculateCenterRect(Screen.width, Screen.height, 200, 200);

            // Register the window. Notice the 3rd parameter
            GUI.Window(0, this.mainWindowRect, DoMyWindow, "Juego finalizado");
        }
    }

    // Make the contents of the window
    void DoMyWindow(int windowID)
    {
        if (GUI.Button(new Rect((this.mainWindowRect.width / 2.0f) - 50, ((this.mainWindowRect.height / 2.0f) - 50) / 2.0f, 100, 50), "Reiniciar"))
        {
            Time.timeScale = 1;
            SceneManager.LoadScene("OficinaFix");
        }
        else if (GUI.Button(new Rect((this.mainWindowRect.width / 2.0f) - 50, (this.mainWindowRect.height / 2.0f) + 25, 100, 50), "Salir"))
        {
            Application.Quit();
        }
    }

    private Rect CalculateCenterRect(float parentWidth, float parentHeight, float width, float height)
    {
        return new Rect((parentWidth / 2.0f) - (width / 2.0f), (parentHeight / 2.0f) - (height / 2.0f), width, height);
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
        Time.timeScale = 0;
        this.endWindowIsActive = true;
    }

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