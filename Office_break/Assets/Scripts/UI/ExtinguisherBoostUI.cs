using UnityEngine;
using UnityEngine.UI;

public class ExtinguisherBoostUI : MonoBehaviour
{
    private Player player;
    private Image fullExtinguisher;

    /// <summary> Called before the first frame update. </summary>
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        fullExtinguisher = this.GetComponent<Image>();
        fullExtinguisher.fillAmount = 0;
    }

    /// <summary> Called after all Update functions have been called. </summary>
    private void LateUpdate()
    {
        if (fullExtinguisher != null)
        {
            fullExtinguisher.fillAmount = player.boostLeft / player.boostMax;
        }
    }
}