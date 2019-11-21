using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        UnityEditor.EditorApplication.isPlaying = false;
    }

    public void ShowScene(int level)
    {
        /*if (currentMenu != null)
            currentMenu.IsOpen = false;
        currentMenu = null;*/
        Application.LoadLevel(level);
    }
}
