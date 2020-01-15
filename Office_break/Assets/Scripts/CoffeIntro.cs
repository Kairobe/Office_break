using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CoffeIntro : MonoBehaviour
{
    private float animationStatus;
    private Image coffeeImg;
    private bool emptying;
    // Start is called before the first frame update
    void Start()
    {
        coffeeImg = this.GetComponent<Image>();
        animationStatus = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) SceneManager.LoadScene("MenuInicial");
        if (animationStatus > 1) emptying = true;
        if (animationStatus <= 0) emptying = false;

        if(emptying) animationStatus -= Time.deltaTime / 10;
        else animationStatus += Time.deltaTime / 10;

        coffeeImg.fillAmount = animationStatus;
    }
}
