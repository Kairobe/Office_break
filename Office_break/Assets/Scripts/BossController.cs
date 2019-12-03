using UnityEngine;
using UnityEngine.AI;

public class BossController : MonoBehaviour
{
    private bool hasDetectedPlayer = false;
    private NavMeshAgent agent;
    private GameObject player;
    private ElevatorController elevatorController;
    private bool elevatorDoorsHasBeenOpened = false;
    private ControladorUi controladorUi;

    [SerializeField]
    private Floor actualFloor;

    private enum Floor { Office = 0, Garden = 1, Cafe = 2 }

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Awake()
    {
        this.player = GameObject.FindGameObjectWithTag("Player");
        this.elevatorController = GameObject.FindGameObjectWithTag("Elevator").GetComponent<ElevatorController>();
        this.controladorUi = GameObject.FindGameObjectWithTag("PropiedadesUi").GetComponent<ControladorUi>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasDetectedPlayer)
        {
            int randomNumber = Random.Range(1, 150);

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

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            this.controladorUi.EndGame();
        }
    }

    private void MoveBossToElevator(Floor currentFloor)
    {
        Debug.Log(currentFloor);

        switch (currentFloor)
        {
            case Floor.Cafe:
                this.transform.position = new Vector3(15.62385f, 0.6f, -15.95f);
                break;
            case Floor.Garden:
                this.transform.position = new Vector3(10.96f, 0.6f, -15.95f);
                break;
            case Floor.Office:
                this.transform.position = new Vector3(14.37f, 0.6f, -15.95f);
                break;
            default:
                break;
        }
    }
}