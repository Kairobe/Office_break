using UnityEngine;
using UnityEngine.UI;

public class CoffeUI : MonoBehaviour
{
    public RectTransform coffeBar;
    private float coffePercentaje;
    public GameObject playerObject;
    private Player player;
    public Image tmp;
    private float animationStatus;
    private bool playerSet = false;

    void Start()
    {
        player = playerObject.GetComponent<Player>();
        animationStatus = 0;
    }

    void LateUpdate()
    {
        coffePercentaje = player.GetCoffeLeftPercentaje();
        coffeBar.localScale = new Vector3(coffePercentaje, 1f, 1f);
        if(player.reloadingCoffe){
            animationStatus += Time.deltaTime;
            if (animationStatus > 1) animationStatus -= 1;
        }else{
            animationStatus = 0;
        }

        tmp.fillAmount = animationStatus;
    }
}