using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField]
    private float _speed = 3.5f;
    [SerializeField]
    private float _boost = 2f;

    public float currentspeed;

    // Start is called before the first frame update
    void Start()
    {
        currentspeed = _speed;
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);
        transform.Translate(direction * currentspeed * Time.deltaTime);
    }

    public void Objeto(GameObject item)
    {
        Debug.Log("He pillado un objeto :D" );
        Debug.Log(item.tag);

        if (item.tag == "Extintor")
        {
           currentspeed = _speed + _boost;
        }
        }
}
