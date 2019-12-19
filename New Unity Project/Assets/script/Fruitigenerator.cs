using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruitigenerator : MonoBehaviour
{
    [SerializeField]
    private GameObject fruiti;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i <= 9; i++)
        {
            Vector3 posInit = new Vector3(Random.Range(-4, 4), 2, Random.Range(-4, 4));
            Instantiate(fruiti, posInit, Quaternion.identity);
        }        
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
