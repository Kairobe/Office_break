using UnityEngine;

public class Extintor : Objeto
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnTriggerEnter(Collider other)
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