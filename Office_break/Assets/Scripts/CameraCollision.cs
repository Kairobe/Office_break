using UnityEngine;

public class CameraCollision : MonoBehaviour
{
    private Vector3 minPosition, maxPosition, posDif, futurePosition;
    public bool moved;

    /// <summary> Called before the first frame update. </summary>
    private void Start()
    {
        moved = false;
    }

    /// <summary> Called after all Update functions have been called. </summary>
    private void LateUpdate()
    {
        minPosition = transform.parent.parent.position;
        maxPosition = transform.parent.position;
        posDif = new Vector3(
            maxPosition.x - minPosition.x,
            maxPosition.y - minPosition.y,
            maxPosition.z - minPosition.z
        );

        RaycastHit hit;

        if (!moved)
        {
            float h = Input.GetAxis("Horizontal");

            if (h != 0f)
            {
                moved = true;
            }
        }

        if (Physics.Linecast(minPosition, maxPosition, out hit) && moved)
        {
            futurePosition = minPosition + new Vector3(
                posDif.x * (hit.distance / 2),
                posDif.y * (hit.distance / 2),
                posDif.z * (hit.distance / 2)
            );
        }
        else
        {
            futurePosition = maxPosition;
        }

        transform.position = futurePosition;
    }
}