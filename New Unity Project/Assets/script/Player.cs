using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private CharacterController characterController;

    public float gravity = 20.0f;
    Vector3 direction = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        float verticalInput = Input.GetAxis("Vertical");
        float horizontalInput = Input.GetAxis("Horizontal");

        if (characterController.isGrounded)
        {
            direction = new Vector3(horizontalInput, 0, verticalInput);
            direction = transform.TransformDirection(direction);
            direction *= 2f;
        }

        direction.y -= gravity * Time.deltaTime;
        characterController.Move(direction * Time.deltaTime);
    }
}
