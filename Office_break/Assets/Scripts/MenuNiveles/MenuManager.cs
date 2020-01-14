using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void Quit()
    {
        Application.Quit();

        //UnityEditor.EditorApplication.isPlaying = false;
    }

    public void ShowScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}