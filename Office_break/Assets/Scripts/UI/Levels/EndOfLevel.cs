using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndOfLevel : MonoBehaviour
{
    private Text text;

    /// <summary> Called before the first frame. </summary>
    private void Start()
    {
        text = GetComponent<Text>();

        SetText();
    }

    /// <summary> Called once per frame. </summary>
    private void Update()
    {
    }

    /// <summary> Quits the <see cref="Application"/>. </summary>
    public void Quit()
    {
        Application.Quit();

        // UnityEditor.EditorApplication.isPlaying = false;
    }

    /// <summary> Shows the scene with the given name. </summary>
    /// <param name="sceneName"> The name of the scene to show. </param>
    public void ShowScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    /// <summary>
    /// Sets the text associated with the different objects collected during the race and some
    /// statistics about it.
    /// </summary>
    private void SetText()
    {
        LevelData levelData = CurrentLevelController.CurrentLevelData;

        switch (text.tag)
        {
            case "FinDeNivelTextClip":
                text.text = $"x{levelData.collectedClips}";

                break;
            case "FinDeNivelTextMaletin":
                text.text = $"x{levelData.collectedBriefcases}";

                break;
            default:
                if (levelData.playerPosition >= 1)
                {
                    text.text = $"Has quedado {levelData.playerPosition}º!";
                }
                else
                {
                    text.text = "El jefe te ha pillado...";
                }

                break;
        }
    }
}