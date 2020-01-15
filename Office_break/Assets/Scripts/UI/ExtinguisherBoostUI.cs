using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExtinguisherBoostUI : MonoBehaviour {

    private Player player;
    private Image fullExtinguisher;
    private float animationStatus;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        fullExtinguisher = this.GetComponent<Image>();
        fullExtinguisher.fillAmount = 0;
    }
    void LateUpdate() {
        if(fullExtinguisher != null) fullExtinguisher.fillAmount = player.boostLeft/player.boostMax;
    }

}
