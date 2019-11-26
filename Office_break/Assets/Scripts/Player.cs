using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    private CharacterController characterController;
    
    [SerializeField]
    private float _baseSpeed = 1.5f;

    [SerializeField]
    private float _boostSpeed = 2f;

    [SerializeField]
    private string _arma = "Tirachinas";
    [SerializeField]
    private GameObject _borradorPrefab;

    [SerializeField]
    public float currentspeed;

    [SerializeField]
    private Vector3 direction;
    private Vector3 rotation;

    [SerializeField] private float coffeSecondsLeft;
    [SerializeField] private float coffeTimeMax = 40;
    [SerializeField] private float coffeSpeed = 2f;
    private bool boostActive = false;

    private Animator anim;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        currentspeed = _baseSpeed;
        characterController = GetComponent<CharacterController>();
        coffeSecondsLeft = coffeTimeMax;
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
  
        direction = new Vector3(verticalInput * Mathf.Sin(transform.eulerAngles.y * 0.01745f), 0.0f, verticalInput * Mathf.Cos(transform.eulerAngles.y * 0.01745f));
        rotation = new Vector3(0, horizontalInput, 0);
        transform.Rotate(rotation * currentspeed * 0.7f);

        currentspeed = _baseSpeed;
        if(boostActive) currentspeed += _boostSpeed;
        if(coffeSecondsLeft != 0f) currentspeed += coffeSpeed;
        characterController.Move(direction * currentspeed * Time.deltaTime);
        coffeSecondsLeft = Mathf.Max(coffeSecondsLeft - Time.deltaTime, 0);

        if(_arma != "None")
        {
            
            if (Input.GetKeyDown(KeyCode.Space))
            {
                anim.SetBool("Disparando", true);
                Disparar();
                anim.SetBool("Disparando", false);
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
        boostActive = true;

        for (int i = 0; i < seconds; i++)
        {
            yield return new WaitForSeconds(1);
        }

        boostActive = false;
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

    public void FillCoffe(float percentaje){
        coffeSecondsLeft = Mathf.Min(coffeTimeMax, coffeSecondsLeft+(percentaje*coffeTimeMax));
    }

}