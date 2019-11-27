using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoffeUI : MonoBehaviour {
    
    public RectTransform coffeBar;
    private float coffePercentaje;
    public GameObject playerObject;
    private Player player;
    private bool playerSet = false;

    void Start() {
        player = playerObject.GetComponent<Player>();
    }

    void LateUpdate() {
        coffePercentaje = player.GetCoffeLeftPercentaje();
        coffeBar.localScale = new Vector3(coffePercentaje, 1f, 1f);
        Debug.Log("UPDATE = " + coffePercentaje);
    }
}
