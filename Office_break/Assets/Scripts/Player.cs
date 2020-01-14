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
    public bool reloadingCoffe;

    [SerializeField] private float coffeSecondsLeft;
    [SerializeField] private float coffeTimeMax = 40;
    [SerializeField] private float coffeSpeed = 2f;
    private bool boostActive = false;
    public float boostLeft = 0;
    public int boostMax = 3;

    private ParticleSystem particulasExtintor;
    private GameObject animationExtintor;

    // Start is called before the first frame update
    void Start()
    {
        currentspeed = _baseSpeed;
        characterController = GetComponent<CharacterController>();
        coffeSecondsLeft = coffeTimeMax;

        this.particulasExtintor = this.GetComponent<ParticleSystem>();
        this.animationExtintor = GameObject.FindGameObjectWithTag("PlayerExtinguisher");
        this.animationExtintor.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        direction = new Vector3(verticalInput * Mathf.Sin(transform.eulerAngles.y * 0.01745f), 0.0f, verticalInput * Mathf.Cos(transform.eulerAngles.y * 0.01745f));
        rotation = new Vector3(0, horizontalInput * 1f, 0);
        transform.Rotate(rotation * currentspeed * 0.7f);

        currentspeed = _baseSpeed;
        if (boostLeft > 0) currentspeed += _boostSpeed;
        if (coffeSecondsLeft != 0f) currentspeed += coffeSpeed;
        characterController.Move(direction * currentspeed * Time.deltaTime);

        if (verticalInput != 0 || horizontalInput != 0) coffeSecondsLeft = Mathf.Max(coffeSecondsLeft - Time.deltaTime, 0);
        boostLeft = Mathf.Max(boostLeft - Time.deltaTime, 0);

        if (_arma != "None")
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Disparar();
            }
        }

        if (reloadingCoffe)
        {
            FillCoffe(.1f * Time.deltaTime);
        }
    }

    public void Objeto(GameObject item)
    {
        if (item != null)
        {
            string tagObjeto = item.tag;

            if (tagObjeto == "Extintor")
            {
                IncreaseSpeed(boostMax);
            }
        }
    }

    public void IncreaseSpeed(int seconds)
    {
        if (seconds == boostMax)
        {
            this.particulasExtintor.Play();
            this.animationExtintor.SetActive(true);
        }

        if (seconds <= 0)
        {
            this.animationExtintor.SetActive(false);
        }

        boostLeft = seconds;
    }

    public void Disparar()
    {
        if (_arma == "Tirachinas")
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");
            GameObject Municion = Instantiate(_borradorPrefab, new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z), Quaternion.identity);
        }
    }

    public Vector3 getDireccion()
    {
        return this.transform.forward;
    }

    public void FillCoffe(float percentaje)
    {
        coffeSecondsLeft = Mathf.Min(coffeTimeMax, coffeSecondsLeft + (percentaje * coffeTimeMax));
    }

    public float GetCoffeLeftSeconds()
    {
        return coffeSecondsLeft;
    }

    public float GetCoffeLeftPercentaje()
    {
        return coffeSecondsLeft / coffeTimeMax;
    }
}