using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoffeMachine : MonoBehaviour
{
    private bool playerReloading, halfReloaded, fullReloaded;
    private float timeStayed;
    private Player player;

    public void OnTriggerEnter(Collider collider) {
        if (collider.gameObject.name == "Player") { 
            playerReloading = true;
            if (player == null) player = collider.transform.GetComponent<Player>();
        }
    }

    public void Update(){
        if(playerReloading){
            timeStayed += Time.deltaTime;
            if(timeStayed >= 2f && !halfReloaded){
                player.FillCoffe(0.5f);
                halfReloaded = true;
            }
            if(timeStayed >= 5f && !fullReloaded){
                player.FillCoffe(1f);
                fullReloaded = true;
            }

        }
    }

    public void OnTriggerExit(Collider collider) {
        if (collider.gameObject.name == "Player") {
            playerReloading = false;
            halfReloaded = false;
            fullReloaded = false;
            timeStayed = 0f;
        }
    }

}
