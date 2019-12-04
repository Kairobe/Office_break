using System.Collections;
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
        texto = GetComponent<Text>();

        SetTexto();
    }

    // Update is called once per frame

    private void SetTexto()
    {
        LevelData datos = CurrentLevelController.CurrentLevelData;

        // Debug.Log(datos.playerAlias);
        textoEscrito = "Enhorabuena " + datos.playerAlias + "!\n \n";
        textoEscrito += "Has conseguido " + datos.collectedBriefcases + " maletines!\n \n";
        textoEscrito += "Tambien has conseguido " + datos.collectedClips + " clips!\n \n";

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

    public void FuncionalidadPorHacer()
    {
        //Debug.Log(datos.playerAlias);
        textoEscrito = "Lo sentimos, esta funcionalidad aun no esta implementada :(";

        //texto.text = textoEscrito;
        texto.GetComponent<Text>().text = textoEscrito;
        StartCoroutine("MantenerMensaje");
    }

    private IEnumerator MantenerMensaje()
    {
        yield return new WaitForSeconds(2);
        SetTexto();
    }
}