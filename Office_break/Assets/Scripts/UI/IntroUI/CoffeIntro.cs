using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CoffeIntro : MonoBehaviour
{
    private float animationStatus;
    private Image coffeeImage;
    private bool emptying;

    /// <summary> Called before the first frame update. </summary>
    private void Start()
    {
        coffeeImage = this.GetComponent<Image>();
        animationStatus = 0;
    }

    /// <summary> Called once per frame. </summary>
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SceneManager.LoadScene("MenuInicial");
        }

        if (animationStatus > 1)
        {
            emptying = true;
        }

        if (animationStatus <= 0)
        {
            emptying = false;
        }

        if (emptying)
        {
            animationStatus -= Time.deltaTime / 10;
        }
        else
        {
            animationStatus += Time.deltaTime / 10;
        }

        coffeeImage.fillAmount = animationStatus;
    }
}