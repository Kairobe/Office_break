using UnityEngine;

public abstract class CollectableObject : Object
{
    private LevelController levelController;

    /// <summary> Called before the first frame update. </summary>
    private void Start()
    {
    }

    /// <summary> Called once per frame. </summary>
    private void Update()
    {
    }

    /// <summary> Called when the script instance is being loaded. </summary>
    private void Awake()
    {
        levelController = GameObject.Find("LevelController").GetComponent<LevelController>();
    }

    /// <summary> Called when the GameObject collides with another GameObject. </summary>
    /// <param name="collider">
    /// The <see cref="Collider"/> with details about the trigger event, such as the name of its GameObject.
    /// </param>
    void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            this.levelController.IncreaseCollectedNumber(this.tag);

            // Deletes the collected object.
            Destroy(this.gameObject);
        }
    }
}