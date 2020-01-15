using UnityEngine;

public class MiniMapUIController : MonoBehaviour
{
    public Transform mapMin, mapMax, player;

    private float x, y;
    private RectTransform marker;

    /// <summary> Called before the first frame update. </summary>
    private void Start()
    {
        marker = GetComponent<RectTransform>();
    }

    /// <summary> Called after all Update functions have been called. </summary>
    private void LateUpdate()
    {
        x = ((player.position.x - mapMin.position.x) / (mapMax.position.x - mapMin.position.x)) * 200;
        y = -((player.position.z - mapMin.position.z) / (mapMax.position.z - mapMin.position.z)) * 200;

        marker.localPosition = new Vector3(x, y, 0);
    }
}