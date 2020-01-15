using UnityEngine;
using UnityEngine.UI;

public class CoffeUIController : MonoBehaviour
{
    [SerializeField]
    public RectTransform coffeBar;

    [SerializeField]
    public GameObject playerObject;

    [SerializeField]
    public Image coffeeBarImage;

    private float coffePercentage;
    private Player player;
    private float animationStatus;

    /// <summary> Called before the first frame. </summary>
    private void Start()
    {
        player = playerObject.GetComponent<Player>();
        animationStatus = 0;
    }

    /// <summary> Called after all Update functions have been called. </summary>
    private void LateUpdate()
    {
        coffePercentage = player.GetCoffeLeftPercentage();
        coffeBar.localScale = new Vector3(coffePercentage, 1f, 1f);

        if (player.reloadingCoffe)
        {
            animationStatus += Time.deltaTime;

            if (animationStatus > 1)
            {
                animationStatus -= 1;
            }
        }
        else
        {
            animationStatus = 0;
        }

        coffeeBarImage.fillAmount = animationStatus;
    }
}