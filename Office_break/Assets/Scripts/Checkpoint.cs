using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [SerializeField]
    private int index;

    private Nivel nivel;

    public int Index
    {
        get
        {
            return this.index;
        }
        set
        {
            this.index = value;
        }
    }

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
        this.nivel = GameObject.FindGameObjectWithTag("Nivel").GetComponent<Nivel>();
    }

    private void OnTriggerEnter(Collider other)
    {
        string tagName = other.tag;

        if (tagName == "Player" || tagName == "Oponent")
        {
            this.nivel.ActivateNextCheckPoint(this.index, other.gameObject);
        }
    }
}