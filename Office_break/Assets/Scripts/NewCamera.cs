using System.Collections.Generic;
using UnityEngine;

public class NewCamera : MonoBehaviour
{
    public float smoothing = 0.125f;
    public Transform follow;
    public Transform player;

    private Vector3 newObjectivePosition;
    private List<Vector3> objectivePositionQueue;

    /// <summary> Called before the first frame update. </summary>
    private void Start()
    {
        objectivePositionQueue = new List<Vector3>();
    }

    /// <summary> Called after all Update functions have been called. </summary>
    private void LateUpdate()
    {
        newObjectivePosition = follow.position;

        if (objectivePositionQueue.Count != 0)
        {
            if (!objectivePositionQueue[objectivePositionQueue.Count - 1].Equals(newObjectivePosition))
            {
                objectivePositionQueue.Add(newObjectivePosition);
            }
        }
        else
        {
            objectivePositionQueue.Add(newObjectivePosition);
        }

        if (objectivePositionQueue.Count >= 15)
        {
            objectivePositionQueue.RemoveAt(0);
        }

        transform.position = Vector3.Lerp(transform.position, objectivePositionQueue[0], smoothing);

        transform.LookAt(player);
    }
}