using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    public void continueLevel()
    {
        canvas.enabled = !canvas.enabled;
        Time.timeScale = canvas.enabled ? 0 : 1;
    }

    public void exit()
    {
        SceneManager.LoadScene("MenuInicial");
    }

    public void volumeMute()
    {

    }

    public void volumeReduce()
    {

    }
    
    public void volumeIncrease()
    {

    }
}
