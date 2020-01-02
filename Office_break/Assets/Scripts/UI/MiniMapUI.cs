using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapUI : MonoBehaviour {    
    public Transform mapMin, mapMax, player; //MIN = Arriba-Izquierda //MAX = Abajo-Derecha 
    public float x, y;
    private RectTransform marker;
    void Start() {
        marker = GetComponent<RectTransform>();
    }

    void LateUpdate() {
        x = ((player.position.x - mapMin.position.x) / (mapMax.position.x - mapMin.position.x))*200;
        y = -((player.position.z - mapMin.position.z) / (mapMax.position.z - mapMin.position.z))*200;
        marker.localPosition = new Vector3(x, y, 0);
    }
}
