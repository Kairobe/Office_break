using UnityEngine;

public class CheckpointController : MonoBehaviour
{
    [SerializeField]
    private int index;

    private LevelController levelController;

    public int Index
    {
        get
        {
            return this.index;
        }
        set
        {
            this.index = value;
        }
    }

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
        this.levelController = GameObject.FindGameObjectWithTag("LevelController").GetComponent<LevelController>();
    }

    /// <summary> Called when the GameObject collides with another GameObject. </summary>
    /// <param name="collider">
    /// The <see cref="Collider"/> with details about the trigger event, such as the name of its GameObject.
    /// </param>
    private void OnTriggerEnter(Collider collider)
    {
        string tagName = collider.tag;

        if (tagName == "Player" || tagName == "Oponent")
        {
            this.levelController.ActivateNextCheckPoint(this.index, collider.gameObject);
        }
    }
}