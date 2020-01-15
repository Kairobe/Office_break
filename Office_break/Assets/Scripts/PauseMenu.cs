using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    private Canvas canvas;
    private Text volumeValue;

    // Start is called before the first frame update
    void Start()
    { 
        canvas = this.GetComponent<Canvas>();
        canvas.enabled = false;
        volumeValue = GameObject.FindGameObjectWithTag("TextoVolumen").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape))
        {
            canvas.enabled = !canvas.enabled;
            Time.timeScale = canvas.enabled ? 0 : 1;
        }
    }

    public void ContinueLevel()
    {
        canvas.enabled = !canvas.enabled;
        Time.timeScale = canvas.enabled ? 0 : 1;
    }

    public void Exit()
    {
        SceneManager.LoadScene("MenuInicial");
    }

    public void VolumeMute()
    {
        AudioListener.volume = 0;
        volumeValue.text = AudioListener.volume * 100 + "";
    }

    public void VolumeReduce()
    {
        AudioListener.volume -= 1;
        volumeValue.text = AudioListener.volume * 100 + "";
    }
    
    public void VolumeIncrease()
    {
        AudioListener.volume += 1;
        volumeValue.text = AudioListener.volume * 100 + "";
    }
}
