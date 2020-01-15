using UnityEngine;

public class Extinguisher : Object
{
    /// <summary> Called before the first frame update. </summary>
    private void Start()
    {
    }

    /// <summary> Called once per frame. </summary>
    private void Update()
    {
    }

    /// <summary> Called when the GameObject collides with another GameObject. </summary>
    /// <param name="collider">
    /// The <see cref="Collider"/> with details about the trigger event, such as the name of its GameObject.
    /// </param>
    void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            Player player = collider.transform.GetComponent<Player>();

            if (player != null)
            {
                player.UseCollectableObject(this.gameObject);
            }

            // Eliminar extintor
            Destroy(this.gameObject);
        }
    }
}