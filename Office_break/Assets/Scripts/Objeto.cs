using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Objeto : MonoBehaviour
{
    public List<Vector3> availablePositions = new List<Vector3>();
    public Vector3 objectPosition = new Vector3();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public abstract void consumePosition();
}
