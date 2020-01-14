using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class DirectedAgent : MonoBehaviour
{
    private NavMeshAgent agent;

    private Rigidbody rigidbodyComponent;
    private bool shouldRotate = false;

    private List<Vector3> levelRoutePoints;

    private int nextIndex = 0;

    private bool isStopped = false;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        rigidbodyComponent = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (isStopped)
        {
            return;
        }

        agent.speed = Random.Range(1.5f, 4f);

        if (agent.remainingDistance < 1)
        {
            agent.SetDestination(levelRoutePoints[++nextIndex % levelRoutePoints.Count]);
        }
    }

    void Start()
    {
        InitializeRoutePoints();
    }

    public void StopAgent()
    {
        StartCoroutine("StopAgentCoroutine");
    }

    private IEnumerator StopAgentCoroutine()
    {
        this.isStopped = true;

        yield return new WaitForSeconds(3);

        agent.isStopped = true;
    }

    private void FixedUpdate()
    {
        if (isStopped)
        {
            return;
        }

        if (shouldRotate)
        {
            StartCoroutine("Rotate");
        }
    }

    private IEnumerator Rotate()
    {
        float originalSpeed = agent.speed;
        rigidbodyComponent.AddTorque(new Vector3(0f, 270f, 0f), ForceMode.Force);
        agent.speed = agent.speed * 0.75f;

        yield return new WaitForSeconds(2);
        rigidbodyComponent.angularVelocity = Vector3.zero;
        agent.speed = originalSpeed;
        shouldRotate = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "MunicionLapiz")
        {
            ////    var destination = agent.destination;

            ////    agent.isStopped = true;

            shouldRotate = true;

            //agent.SetDestination(destination);
        }
    }

    private void InitializeRoutePoints()
    {
        string currentScene = SceneManager.GetActiveScene().name;

        switch (currentScene)
        {
            case "OficinaFix":
                this.levelRoutePoints = new List<Vector3> {
                    new Vector3(10.76f, 0.0f, -15.89f),
                    new Vector3(0.95f, 0.0f, -21.27f),
                    new Vector3(0.95f, 0.0f, -31.19f),
                    new Vector3(17.1f, 0.0f, -32.78f),
                    new Vector3(17.53f, 0.0f, -21.31f),
                };

                break;
            case "Cafeteria":
                this.levelRoutePoints = new List<Vector3> {
                    new Vector3(10f, 0.0f, 1.5f),
                    new Vector3(5.5f, 0.0f, -9.4f),
                    new Vector3(17f, 0.0f, -18f),
                    new Vector3(23.5f, 0.0f, 2.5f),
                };

                break;
            case "Jardin":
            //// TODO: Complete the points of the level associated with the scene named 'Jardin'.
            default:
                break;
        }
    }
}