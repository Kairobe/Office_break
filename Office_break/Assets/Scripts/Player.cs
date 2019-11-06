using UnityEngine;

internal enum ControlType
{
    strafe, tank, car,
}

public class Player : MonoBehaviour
{
    CharacterController characterController;

    [SerializeField]
    private float _speed = 3.5f;

    [SerializeField]
    private float _boost = 2f;

    [SerializeField]
    private ControlType controlType = ControlType.tank;

    [SerializeField]
    public float currentspeed;

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
        Vector3 direction;
        Vector3 rotation;

        switch (controlType)
        {
            case ControlType.strafe:
                direction = new Vector3(horizontalInput, 0, verticalInput);
                transform.Translate(direction * currentspeed * Time.deltaTime);
                break;
            case ControlType.tank:
                direction = new Vector3(0, horizontalInput, verticalInput);
                rotation = new Vector3(0, horizontalInput, 0);
                //transform.Translate(direction * currentspeed * Time.deltaTime);
                //transform.Rotate(rotation * currentspeed * 0.7f);
                Vector3 moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
                characterController.Move(moveDirection * currentspeed * Time.deltaTime);
                //TODO Evitar marcha atras
                break;
            case ControlType.car:
                direction = new Vector3(0, 0, verticalInput);
                rotation = new Vector3(0, horizontalInput, 0);
                transform.Translate(direction * currentspeed * Time.deltaTime);
                transform.Rotate(rotation * currentspeed * 0.7f * verticalInput);
                break;
            default:
                Debug.Log("No se como lo has hecho, pero no hay elegido un tipo de controles");
                break;
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
                currentspeed = _speed + _boost;
            }
        }
    }
}