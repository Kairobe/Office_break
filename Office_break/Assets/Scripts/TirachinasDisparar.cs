using UnityEngine;

public class TirachinasDisparar : MonoBehaviour
{
    [SerializeField]
    private float _speed = 10f;

    // Start is called before the first frame update
    [SerializeField]
    private Vector3 direction;

    private float rotation;

 

    void Start()
    {
        Player player;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        //transform.Rotate(new Vector3(Mathf.Cos(direction[0]), 0, Mathf.Sin(direction[2])));
        direction = player.getDireccion();



        Object.Destroy(gameObject, 3.0f);
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(direction);
        //direction = new Vector3(1, 0, 1);

        transform.Translate(direction * _speed * Time.deltaTime);
    
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemigo")
        {
            Enemigo enemigo;
            enemigo = GameObject.FindGameObjectWithTag("Enemigo").GetComponent<Enemigo>();
            enemigo.hitwithArrow();
        }
        else if (!(other.tag == "Player"))
        {
            Destroy(this.gameObject);
        }

        //Poner métodos a invocar
    }
}