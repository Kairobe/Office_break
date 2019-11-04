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
    public bool tankControls;

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
        if(tankControls){
            Vector3 direction = new Vector3(0, 0, verticalInput);
            Vector3 rotation = new Vector3(0, horizontalInput, 0);
            transform.Translate(direction * currentspeed * Time.deltaTime);
            transform.Rotate(rotation * currentspeed * 0.25f);
            if(gameObject.transform.rotation.y > 180) gameObject.transform.rotation.SetAxisAngle(new Vector3(0, 1, 0), 180);
        }else{
            Vector3 direction = new Vector3(horizontalInput, 0, verticalInput);
            transform.Translate(direction * currentspeed * Time.deltaTime);
        }
    }

    public void Objeto(GameObject item)
    {
        if (item != null) { 
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
