using UnityEngine;
using UnityEngine.SceneManagement;

public class InventoryWindowManager : MonoBehaviour
{
    [SerializeField]
    private GameObject mainMenuButton;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void NavigateToMainMenu()
    {
        SceneManager.LoadScene("MenuInicial");
    }
}