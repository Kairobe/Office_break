using System.Collections;
using UnityEngine;

internal enum ControlType
{
    strafe, tank, car,
}

public class Player : MonoBehaviour
{
    private CharacterController characterController;

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
                direction = new Vector3(horizontalInput, 0.0f, verticalInput);
                characterController.Move(direction * currentspeed * Time.deltaTime);
                break;
            case ControlType.tank:
                direction = new Vector3(verticalInput * Mathf.Sin(transform.eulerAngles.y * 0.01745f), 0.0f, verticalInput * Mathf.Cos(transform.eulerAngles.y * 0.01745f));
                rotation = new Vector3(0, horizontalInput, 0);
                transform.Rotate(rotation * currentspeed * 0.7f);
                characterController.Move(direction * currentspeed * Time.deltaTime);
                break;
            case ControlType.car:
                direction = new Vector3(verticalInput * Mathf.Sin(transform.eulerAngles.y * 0.01745f), 0.0f, verticalInput * Mathf.Cos(transform.eulerAngles.y * 0.01745f));
                rotation = new Vector3(0, horizontalInput, 0);
                transform.Rotate(rotation * currentspeed * 0.7f * verticalInput);
                characterController.Move(direction * currentspeed * Time.deltaTime);
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
}