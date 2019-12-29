using UnityEngine;

public class CoffeMachine : MonoBehaviour
{
    private Player player;

    public void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.name == "Player")
        {
            if (player == null) player = collider.transform.GetComponent<Player>();
            player.reloadingCoffe = true;
        }
    }

    public void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.name == "Player")
        {
            player.reloadingCoffe = false;
        }
    }
}