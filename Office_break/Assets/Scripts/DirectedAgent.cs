﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class DirectedAgent : MonoBehaviour
{
    private NavMeshAgent agent;
    private Rigidbody rigidbodyComponent;
    private bool isStopped = false, shouldRotate = false;
    private List<Vector3> levelRoutePoints;
    private int nextIndex = 0;

    /// <summary> Called when the script instance is being loaded. </summary>
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        rigidbodyComponent = GetComponent<Rigidbody>();
    }

    /// <summary> Called before the first frame update. </summary>
    private void Start()
    {
        InitializeRoutePoints();
    }

    /// <summary> Called once per frame. </summary>
    private void Update()
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

    /// <summary> Frame-rate independent message for physics calculations. </summary>
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

    /// <summary> Called when the GameObject collides with another GameObject. </summary>
    /// <param name="collider">
    /// The <see cref="Collider"/> with details about the trigger event, such as the name of its GameObject.
    /// </param>
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("PencilMunition"))
        {
            shouldRotate = true;
        }
    }

    /// <summary> Stops the navigation of the <see cref="NavMeshAgent"/>. </summary>
    public void StopAgent()
    {
        StartCoroutine("StopAgentCoroutine");
    }

    /// <summary> A coroutine that stops the <see cref="NavMeshAgent"/>. </summary>
    /// <returns>
    /// An empty <see cref="IEnumerator"/> that enables this method to be called as a coroutine.
    /// </returns>
    private IEnumerator StopAgentCoroutine()
    {
        this.isStopped = true;

        yield return new WaitForSeconds(3);

        agent.isStopped = true;
    }

    /// <summary> Starts rotating the current <see cref="NavMeshAgent"/>. </summary>
    /// <returns>
    /// An empty <see cref="IEnumerator"/> that enables this method to be called as a coroutine.
    /// </returns>
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

    /// <summary> Initializes the routing points depending on the current <see cref="Scene"/>. </summary>
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
                this.levelRoutePoints = new List<Vector3> {
                    new Vector3(9.5f, 0f, -23f),
                    new Vector3(14f, 0f, -23f),
                    new Vector3(16.3f, 0f, -23f),
                    new Vector3(18.84f, 0f, -21f),
                    new Vector3(19.5f, 0f, -17f),
                    new Vector3(19.5f, 0f, -14f),
                    new Vector3(18.5f, 0f, -13f),
                    new Vector3(11f, 0f, -13f),
                    new Vector3(10.5f, 0f, -18f),
                    new Vector3(8f, 0f, -18.5f),
                    new Vector3(5.5f, 0f, -18.5f),
                    new Vector3(4.5f, 0f, -14f),
                    new Vector3(4.5f, 0f, -3.8f),
                    new Vector3(18f, 0f, -3.8f),
                    new Vector3(18.5f, 0f, -3f),
                    new Vector3(18.5f, 0f, -0.5f),
                    new Vector3(13f, 0f, -0.85f),
                    new Vector3(10.5f, 0f, -0.85f),
                    new Vector3(9.15f, 0f, 2f),
                    new Vector3(7.5f, 0f, 2.75f),
                    new Vector3(4f, 0f, 1.7f),
                    new Vector3(1f, 0f, -1.25f),
                    new Vector3(1.65f, 0f, -17.8f),
                    new Vector3(1.65f, 0f, -22f),
                };

                break;
            default:
                break;
        }
    }
}