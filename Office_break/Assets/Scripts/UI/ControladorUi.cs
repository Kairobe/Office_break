using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ControladorUi : MonoBehaviour
{
    public Text clipCountText, timeCountText, maletinCountText;

    private int minutes, seconds = 0;

    public void UpdateObjectCount(string objectType, int numberOfObjectsCollected)
    {
        string newText = $"x{numberOfObjectsCollected}";

        switch (objectType)
        {
            case "Clip":
                this.clipCountText.text = newText;

                break;
            case "Maletin":
                this.maletinCountText.text = newText;

                break;
            default:
                break;
        }
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