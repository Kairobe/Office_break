using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    private GameObject _extintorPrefab;
    [SerializeField]
    private List<Vector3> _posicionesExtintor;
    // Start is called before the first frame update
    void Start()
    {
        InicializarExtintores();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void InicializarExtintores()
    {
        _posicionesExtintor.Add(new Vector3(1, 1, 1));
        _posicionesExtintor.Add(new Vector3(0, 0, 0));
        Vector3 posInit = _posicionesExtintor[Random.Range(0, _posicionesExtintor.Count)];
        GameObject newEnemy = Instantiate(_extintorPrefab, posInit, Quaternion.identity);
    }
}
