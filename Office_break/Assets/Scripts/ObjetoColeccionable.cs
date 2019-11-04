using UnityEngine;

public abstract class ObjetoColeccionable : Objeto
{
    private Nivel nivel;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void Awake()
    {
        nivel = GameObject.Find("Nivel").GetComponent<Nivel>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            this.nivel.IncreaseCollectedNumber(this.tag);

            //Eliminar extintor
            Destroy(this.gameObject);
        }
    }
}