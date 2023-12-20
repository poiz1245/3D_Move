using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.UIElements;

public class BasicMove : MonoBehaviour
{
    [SerializeField] Rigidbody rigid;

    [SerializeField] float moveSpeed;
    [SerializeField] float mouseSensitivity;

    float horizontalInput;
    float verticalInput;
    float yawInput;

    void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
        yawInput = Input.GetAxisRaw("Mouse X");
    }

    private void FixedUpdate()
    {
        //Move
        Vector3 moveDir = new Vector3(horizontalInput, rigid.velocity.y, verticalInput);
        Vector3 targetVelocity = transform.TransformDirection(moveDir) * moveSpeed;
        Vector3 velocityChange = (targetVelocity - rigid.velocity); 

        //Rotation
        Vector3 rotationAmount = new Vector3(0, yawInput * mouseSensitivity, 0);
        Quaternion deltaRotation = Quaternion.Euler(rotationAmount);

        Movement(velocityChange);
        Rotation(deltaRotation);
    }

    private void Rotation(Quaternion deltaRotation)
    {
        rigid.MoveRotation(rigid.rotation * deltaRotation);


        if (yawInput == 0)
        {
            rigid.angularVelocity = Vector3.zero;
        }
    }

    private void Movement(Vector3 velocityChange)
    {
        rigid.AddForce(velocityChange, ForceMode.VelocityChange);
    }
}
