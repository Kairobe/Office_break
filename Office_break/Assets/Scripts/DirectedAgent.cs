using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DirectedAgent : MonoBehaviour
{
    private NavMeshAgent agent;

    private List<Vector3> levelOneRoute = new List<Vector3> {
        new Vector3(10.76f, 0.2f, -15.89f),
        new Vector3(0.95f, 0.2f, -21.27f),
        new Vector3(0.95f, 0.2f, -31.19f),
        new Vector3(17.1f, 0.2f, -32.78f),
        new Vector3(17.53f, 0.2f, -21.31f)
    };

    private int nextIndex = 0;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        agent.speed = Random.Range(2.5f, 4f);

        if (agent.remainingDistance < 3)
        {
            agent.SetDestination(levelOneRoute[++nextIndex % levelOneRoute.Count]);
        }
    }

    void Start()
    {
    }
}