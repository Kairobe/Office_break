using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FinDeNivel : MonoBehaviour
{
    private string textoEscrito;
    private Text texto;
    // Start is called before the first frame update
    void Start()
    {
        //LevelData levelData = new LevelData("admin", 23, 34);
        //DataManager.SaveData(levelData);


        setTexto();

    }

    // Update is called once per frame

    private void setTexto()
    {
        LevelData datos = DataManager.LoadData("admin");
        Debug.Log(datos.playerAlias);
        textoEscrito = "Enhorabuena " + datos.playerAlias + "!\n \n";
        textoEscrito += "Has conseguido " + datos.collectedBriefcases + " maletines!\n \n";
        textoEscrito += "Tambien has conseguido " + datos.collectedClips + " clips!\n \n";
        texto = GetComponent<Text>();
        texto.text = textoEscrito;
    }
    void Update()
    {
        
    }
    public void Quit()
    {
        Application.Quit();
        UnityEditor.EditorApplication.isPlaying = false;
    }

    public void ShowScene(string sceneName)
    {
        /*if (currentMenu != null)
            currentMenu.IsOpen = false;
        currentMenu = null;*/
        SceneManager.LoadScene(sceneName);
    }

}
