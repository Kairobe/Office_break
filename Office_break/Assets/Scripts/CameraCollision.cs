using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCollision : MonoBehaviour
{

    public float minDistance = 1f;
    public float maxDistance = 4f;
    public float smooth = 10f;
    public float errorCorrection = 0.9f;
    Vector3 dollyDir;
    public Vector3 dollyDirAdj;
    public float distance;

    void Start() {

    }
    void Awake() {
        dollyDir = transform.localPosition.normalized;
        distance = transform.localPosition.magnitude;
    }
    void Update() {
        Vector3 futurePos = transform.parent.TransformPoint (dollyDir * maxDistance);
        RaycastHit hit;

        if (Physics.Linecast (transform.parent.position, futurePos, out hit))
            distance = Mathf.Clamp (hit.distance * errorCorrection, minDistance, maxDistance);
        else
            distance = maxDistance;

        transform.localPosition = Vector3.Lerp(transform.localPosition, dollyDir * distance, Time.deltaTime * smooth);
    }
}
