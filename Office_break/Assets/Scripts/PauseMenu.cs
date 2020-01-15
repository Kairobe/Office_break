using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    private Canvas canvas;

    /// <summary> Called before the first frame update. </summary>
    private void Start()
    {
        canvas = this.GetComponent<Canvas>();
        canvas.enabled = false;
    }

    /// <summary> Called once per frame </summary>
    private void Update()
    {
        if ((Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape)) && canvas != null)
        {
            canvas.enabled = !canvas.enabled;
            Time.timeScale = canvas.enabled ? 0 : 1;
        }
    }

    /// <summary> Resumes the level. </summary>
    public void ContinueLevel()
    {
        if(canvas != null)
        {
            canvas.enabled = !canvas.enabled;
            Time.timeScale = canvas.enabled ? 0 : 1;
        }        
    }

    /// <summary> Exits to the main menu. </summary>
    public void Exit()
    {
        SceneManager.LoadScene(SceneNames.MainMenu);
    }

    /// <summary> Mutes the volume. </summary>
    public void VolumeMute()
    {
        AudioListener.volume = 0;
    }

    /// <summary> Reduces the volume. </summary>
    public void VolumeReduce()
    {
        if (AudioListener.volume > 0)
        {
            AudioListener.volume -= (float)0.01;
        }
    }

    /// <summary> Increases the volume. </summary>
    public void VolumeIncrease()
    {
        if (AudioListener.volume < 1)
        {
            AudioListener.volume += (float)0.01;
        }
    }
}