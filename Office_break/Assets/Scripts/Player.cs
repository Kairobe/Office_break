using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float baseSpeed = 1.5f, boostSpeed = 2f, coffeSpeed = 2f;

    [SerializeField]
    private string weapon = "Tirachinas";

    [SerializeField]
    private GameObject draftPrefab;

    [SerializeField]
    public float currentspeed;

    [SerializeField]
    private Vector3 direction;

    [SerializeField]
    private float coffeSecondsLeft;

    [SerializeField]
    private float coffeTimeMax = 40;

    public bool reloadingCoffe;
    public float boostLeft = 0;
    public int boostMax = 3;

    private Vector3 rotation;
    private bool boostActive = false;
    private CharacterController characterController;
    private ParticleSystem extinguisherParticles;
    private AudioSource extinguisherSound;
    private GameObject extinguisherAnimation;

    /// <summary> Called before the first frame update. </summary>
    private void Start()
    {
        currentspeed = baseSpeed;
        characterController = GetComponent<CharacterController>();
        coffeSecondsLeft = coffeTimeMax;

        this.extinguisherParticles = GameObject.FindGameObjectWithTag("ExtinguisherParticleSystem").GetComponent<ParticleSystem>();
        this.extinguisherAnimation = GameObject.FindGameObjectWithTag("PlayerExtinguisher");
        this.extinguisherSound = extinguisherAnimation.GetComponent<AudioSource>();
        this.extinguisherAnimation.SetActive(false);
    }

    /// <summary> Called once per frame. </summary>
    private void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        direction = new Vector3(verticalInput * Mathf.Sin(transform.eulerAngles.y * 0.01745f), 0.0f, verticalInput * Mathf.Cos(transform.eulerAngles.y * 0.01745f));
        rotation = new Vector3(0, horizontalInput * 1f, 0);

        transform.Rotate(rotation * currentspeed * 0.7f);

        currentspeed = baseSpeed;

        if (boostLeft > 0)
        {
            currentspeed += boostSpeed;
        }

        if (coffeSecondsLeft != 0f)
        {
            currentspeed += coffeSpeed;
        }

        characterController.Move(direction * currentspeed * Time.deltaTime);

        if (verticalInput != 0 || horizontalInput != 0)
        {
            coffeSecondsLeft = Mathf.Max(coffeSecondsLeft - Time.deltaTime, 0);
        }

        boostLeft = Mathf.Max(boostLeft - Time.deltaTime, 0);

        if (boostLeft <= 0 && this.extinguisherAnimation != null)
        {
            this.extinguisherAnimation.SetActive(false);
            this.extinguisherParticles.Stop();
            this.extinguisherSound.Stop();
        }

        if (weapon != "None")
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Shoot();
            }
        }

        if (reloadingCoffe)
        {
            FillCoffe(.1f * Time.deltaTime);
        }
    }

    /// <summary> Uses the given <see cref="CollectableObject"/>. </summary>
    /// <param name="gameObject"> The <see cref="CollectableObject"/> to use. </param>
    public void UseCollectableObject(GameObject gameObject)
    {
        if (gameObject != null && gameObject.CompareTag("Extinguiser"))
        {
            IncreaseSpeed(boostMax);
        }
    }

    /// <summary> Gets the current player direction. </summary>
    /// <returns> The current player direction. </returns>
    public Vector3 GetDirection()
    {
        return this.transform.forward;
    }

    /// <summary> Fills the coffee in the given percentage. </summary>
    /// <param name="percentage"> The percentage to increase the coffee on. </param>
    private void FillCoffe(float percentage)
    {
        coffeSecondsLeft = Mathf.Min(coffeTimeMax, coffeSecondsLeft + (percentage * coffeTimeMax));
    }

    /// <summary> Gets the remaining coffee percentage. </summary>
    /// <returns> The remaining coffee percentage. </returns>
    public float GetCoffeLeftPercentage()
    {
        return coffeSecondsLeft / coffeTimeMax;
    }

    /// <summary> Increases the speed of the player during the given seconds. </summary>
    /// <param name="seconds"> The seconds to increase the speed. </param>
    private void IncreaseSpeed(int seconds)
    {
        if (seconds == boostMax)
        {
            this.extinguisherParticles.Play();
            this.extinguisherAnimation.SetActive(true);
            this.extinguisherSound.Play();
        }

        boostLeft = seconds;
    }

    /// <summary> Shoots with the active weapon. </summary>
    private void Shoot()
    {
        if (weapon == "Tirachinas")
        {
            Instantiate(draftPrefab, new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z), Quaternion.identity);
        }
    }
}