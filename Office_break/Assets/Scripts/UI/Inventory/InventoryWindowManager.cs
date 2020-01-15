using UnityEngine;
using UnityEngine.SceneManagement;

public class InventoryWindowManager : MonoBehaviour
{
    /// <summary> Called before the first frame update. </summary>
    private void Start()
    {
    }

    /// <summary> Called once per frame. </summary>
    private void Update()
    {
    }

    /// <summary> Loads the Scene associated with the Main Menu window. </summary>
    public void NavigateToMainMenu()
    {
        SceneManager.LoadScene(SceneNames.MainMenu);
    }
}