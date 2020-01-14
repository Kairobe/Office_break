using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtinguisherBoostUI : MonoBehaviour {

    public RectTransform left;
    public Player player;
    void Update() {
        float timeLeft = player.boostLeft / player.boostMax;
        left.localScale = new Vector3(timeLeft, 1f, 1f);
        if (timeLeft == 0) this.transform.position = new Vector3(this.transform.position.x, -2000, this.transform.position.z);
        else this.transform.position = new Vector3(this.transform.position.x, 200, this.transform.position.z);
    }

}
