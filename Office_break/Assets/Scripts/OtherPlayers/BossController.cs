using UnityEngine;
using UnityEngine.AI;

public class BossController : MonoBehaviour
{
    private bool hasDetectedPlayer = false, elevatorDoorsHasBeenOpened = false, tutorial = false;
    private NavMeshAgent agent;
    private GameObject player;
    private ElevatorController elevatorController;
    private CommonUIController uiController;

    [SerializeField]
    private Floor actualFloor;

    private enum Floor { Office = 0, Garden = 1, Cafe = 2 }

    /// <summary> Called before the first frame update. </summary>
    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    /// <summary> Called when the script instance is being loaded. </summary>
    private void Awake()
    {
        this.player = GameObject.FindGameObjectWithTag("Player");
        this.elevatorController = GameObject.FindGameObjectWithTag("Elevator").GetComponent<ElevatorController>();
        this.uiController = GameObject.FindGameObjectWithTag("PropiedadesUi").GetComponent<CommonUIController>();
    }

    /// <summary> Called once per frame. </summary>
    private void Update()
    {
        if (!tutorial)
        {
            if (!hasDetectedPlayer)
            {
                int randomNumber = Random.Range(1, 3000);

                if (randomNumber == 30)
                {
                    this.hasDetectedPlayer = true;
                }
            }
            else
            {
                Vector3 playerPosition = this.player.transform.position;

                if (Vector3.Distance(playerPosition, transform.position) < 5)
                {
                    if (!elevatorDoorsHasBeenOpened)
                    {
                        this.elevatorController.ManageDoors(true);
                    }

                    this.agent.SetDestination(playerPosition);
                }
            }
        }
    }

    /// <summary> Activates the tutorial mode. </summary>
    public void ActivateTutorialMode()
    {
        tutorial = true;
    }

    /// <summary> Called when the GameObject collides with another GameObject. </summary>
    /// <param name="collider">
    /// The <see cref="Collider"/> with details about the trigger event, such as the name of its GameObject.
    /// </param>
    private void OnTriggerEnter(Collider collider)
    {
        if (!tutorial)
        {
            if (collider.CompareTag("Player"))
            {
                this.uiController.GameOver();
            }
        }
    }
}