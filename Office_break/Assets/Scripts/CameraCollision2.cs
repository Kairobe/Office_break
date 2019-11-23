using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCollision2 : MonoBehaviour
{
    [SerializeField] private float errorCorrection = 0.9f;
    private Vector3 minPos, maxPos, posDif, futurePos;

    void Start() {
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

        if (Physics.Linecast(maxPos, minPos, out hit)){
            futurePos = maxPos - new Vector3(
                posDif.x * (hit.distance/2 + .1f),
                posDif.y * (hit.distance/2 + .1f),
                posDif.z * (hit.distance/2 + .1f)
            );
        } else
            futurePos = maxPos - (posDif*.1f);

        transform.position = futurePos;

        //transform.position = Vector3.Lerp(transform.position, futurePos, Time.deltaTime * smooth);
    }
}
