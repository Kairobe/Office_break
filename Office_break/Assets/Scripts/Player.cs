using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    private CharacterController characterController;
    
    [SerializeField]
    private float _speed = 3.5f;

    [SerializeField]
    private float _boost = 2f;

    [SerializeField]
    private string _arma = "Tirachinas";
    [SerializeField]
    private GameObject _borradorPrefab;

    [SerializeField]
    public float currentspeed;

    [SerializeField]
    private Vector3 direction;
    private Vector3 rotation;


    // Start is called before the first frame update
    void Start()
    {
        currentspeed = _speed;
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
  


        direction = new Vector3(verticalInput * Mathf.Sin(transform.eulerAngles.y * 0.01745f), 0.0f, verticalInput * Mathf.Cos(transform.eulerAngles.y * 0.01745f));
        rotation = new Vector3(0, horizontalInput, 0);
        transform.Rotate(rotation * currentspeed * 0.7f);
        characterController.Move(direction * currentspeed * Time.deltaTime);

        if(_arma != "None")
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Disparar();
            }
        }

    }

    public void Objeto(GameObject item)
    {
        if (item != null)
        {
            Debug.Log("He pillado un objeto :D");
            Debug.Log(item.tag);

            string tagObjeto = item.tag;

            if (tagObjeto == "Extintor")
            {
                var coroutine = IncreaseSpeed(3);

                StartCoroutine(coroutine);
            }
        }
    }

    public IEnumerator IncreaseSpeed(int seconds)
    {
        currentspeed = _speed + _boost;

        for (int i = 0; i < seconds; i++)
        {
            yield return new WaitForSeconds(1);
        }

        currentspeed = _speed;
    }

    public void Disparar()
    {
        if(_arma == "Tirachinas")
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");
            GameObject Municion = Instantiate(_borradorPrefab, new Vector3(transform.position.x,transform.position.y,transform.position.z), Quaternion.identity);

            
        }
    }

    public Vector3 getDireccion()
    {
 

            return this.transform.forward;
    }

}