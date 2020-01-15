using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    private GameObject extinguiserPrefab;

    [SerializeField]
    private List<Vector3> extinguiserPositions;

    /// <summary> Called before the first frame update. </summary>
    private void Start()
    {
        InicializarExtintores();
    }

    /// <summary> Called once per frame. </summary>
    private void Update()
    {
    }

    void InicializarExtintores()
    {
        extinguiserPositions.Add(new Vector3(1, 1, 1));
        extinguiserPositions.Add(new Vector3(0, 0, 0));
        Vector3 initialPosition = extinguiserPositions[Random.Range(0, extinguiserPositions.Count)];
        Instantiate(extinguiserPrefab, initialPosition, extinguiserPrefab.transform.rotation);
    }
}