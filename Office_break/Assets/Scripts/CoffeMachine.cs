using UnityEngine;

public class CoffeMachine : MonoBehaviour
{
    private Player player;

    /// <summary> Called when the GameObject collides with another GameObject. </summary>
    /// <param name="collider">
    /// The <see cref="Collider"/> with details about the trigger event, such as the name of its GameObject.
    /// </param>
    public void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.name == "Player")
        {
            if (player == null)
            {
                player = collider.transform.GetComponent<Player>();
            }

            player.reloadingCoffe = true;
        }
    }

    /// <summary> Called when the other Game Object Collider has stopped touching the trigger. </summary>
    /// <param name="collider">
    /// The <see cref="Collider"/> with details about the trigger event, such as the name of its GameObject.
    /// </param>
    public void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.name == "Player")
        {
            player.reloadingCoffe = false;
        }
    }
}