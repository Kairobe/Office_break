using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    private Canvas canvas;
    
    // Start is called before the first frame update
    void Start()
    { 
        canvas = this.GetComponent<Canvas>();
        canvas.enabled = false;
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
    }

    public void VolumeReduce()
    {
        if(AudioListener.volume > 0)
        {
            AudioListener.volume -= (float)0.01;
        }        
    }
    
    public void VolumeIncrease()
    {
        if(AudioListener.volume < 1)
        {
            AudioListener.volume += (float)0.01;
        }        
    }
}
