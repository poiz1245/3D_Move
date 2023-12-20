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

    float moveX;
    float moveZ;
    float rotationY;

    void Update()
    {
        moveX = Input.GetAxisRaw("Horizontal");
        moveZ = Input.GetAxisRaw("Vertical");
        rotationY = Input.GetAxis("Mouse X");
    }

    private void FixedUpdate()
    {
        //Move
        Vector3 moveInput = new Vector3(moveX, rigid.velocity.y, moveZ);
        Vector3 targetVelocity = transform.TransformDirection(moveInput) * moveSpeed;
        Vector3 velocityChange = (targetVelocity - rigid.velocity); //현재속도와 목표속도 사이의 차이 계산

        //Rotation
        Vector3 moveDir = new Vector3(0, rotationY * mouseSensitivity, 0);
        Quaternion deltaRotation = Quaternion.Euler(moveDir);

        Move(velocityChange);
        Rotation(deltaRotation);
    }

    private void Rotation(Quaternion deltaRotation)
    {
        rigid.MoveRotation(rigid.rotation * deltaRotation);


        if (rotationY == 0)
        {
            rigid.angularVelocity = Vector3.zero;
        }
    }

    private void Move(Vector3 velocityChange)
    {
        rigid.AddForce(velocityChange, ForceMode.VelocityChange);
    }
}
