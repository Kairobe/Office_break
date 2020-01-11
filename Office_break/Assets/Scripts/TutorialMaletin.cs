using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialMaletin : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            TutorialNivel tn = GameObject.FindObjectOfType(typeof(TutorialNivel)) as TutorialNivel;
            tn.pillarMaletin();
            //Eliminar extintor
            Destroy(this.gameObject);
        }
    }
}
