using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Extintor : MonoBehaviour
{
    [SerializeField]
    private List<Vector3> posiciones;


    // Start is called before the first frame update
    void Start()
    {
        posiciones.Add( new Vector3(1, 1, 1));
        posiciones.Add(new Vector3(0, 0, 0));
        //Vector3 posInit = posiciones[Random.Range(0, posiciones.Length-1)];
        Debug.Log(posiciones.ToString());
        transform.position = posiciones[0];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>();

            if (player != null)
            {
                //Invocar método para que player tenga extintor
                player.Objeto(this.gameObject);
            }

            //Eliminar extintor
            Destroy(this.gameObject);
        }
    }
}
