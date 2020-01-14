using UnityEngine;

public class CoffeUI : MonoBehaviour
{
    public RectTransform coffeBar;
    private float coffePercentaje;
    public GameObject playerObject;
    private Player player;
    public Transform reloadingUI;
    private bool playerSet = false;

    void Start()
    {
        player = playerObject.GetComponent<Player>();
        reloadingUI.localPosition = new Vector3(reloadingUI.localPosition.x, -2000, reloadingUI.localPosition.z);
    }

    void LateUpdate()
    {
        coffePercentaje = player.GetCoffeLeftPercentaje();
        coffeBar.localScale = new Vector3(coffePercentaje, 1f, 1f);
        if (player.reloadingCoffe)
        {
            reloadingUI.localPosition = new Vector3(reloadingUI.localPosition.x, -20, reloadingUI.localPosition.z);
        }
        else
        {
            reloadingUI.localPosition = new Vector3(reloadingUI.localPosition.x, -2000, reloadingUI.localPosition.z);
        }
    }
}