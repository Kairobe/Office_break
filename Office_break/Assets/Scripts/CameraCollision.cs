using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCollision : MonoBehaviour
{
    private Vector3 minPos, maxPos, posDif, futurePos;
    public bool moved;
    private int consecutiveHits;

    void Start() {
        moved = false;
        consecutiveHits = 0;
    }
    void Update() {

        minPos = transform.parent.parent.position;
        maxPos = transform.parent.position;
        posDif = new Vector3(
            maxPos.x - minPos.x,
            maxPos.y - minPos.y,
            maxPos.z - minPos.z
        );

        RaycastHit hit;

        if(!moved){
            float h = Input.GetAxis("Horizontal");
            if (h != 0f) moved = true;
        }

        if (Physics.Linecast(maxPos, minPos, out hit) && moved){
            if (consecutiveHits <= 2){
                consecutiveHits++;
                futurePos = maxPos - (posDif*.1f);
            } else 
                futurePos = maxPos - new Vector3(
                    posDif.x * (hit.distance/2 + .1f),
                    posDif.y * (hit.distance/2 + .1f),
                    posDif.z * (hit.distance/2 + .1f)
                );
        } else {
            futurePos = maxPos - (posDif*.1f);
            consecutiveHits = 0;
        }

        transform.position = futurePos;
    }
}
