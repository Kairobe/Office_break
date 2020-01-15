using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    /// <summary> Called before the first frame update. </summary>
    private void Start()
    {
    }

    /// <summary> Called once per frame. </summary>
    private void Update()
    {
    }

    /// <summary> Quits the Application. </summary>
    public void Quit()
    {
        Application.Quit();

        //UnityEditor.EditorApplication.isPlaying = false;
    }

    /// <summary> Shows the <see cref="Scene"/> with the given name. </summary>
    /// <param name="sceneName"> The name of the <see cref="Scene"/> to show. </param>
    public void ShowScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}