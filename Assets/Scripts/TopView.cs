using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.UIElements;

public class TopView : MonoBehaviour
{
    [SerializeField] Rigidbody rigid;

    [SerializeField] float moveSpeed;
    [SerializeField] float mouseSensitivity;

    float moveX;
    float moveZ;

    void Update()
    {
        moveX = Input.GetAxisRaw("Horizontal");
        moveZ = Input.GetAxisRaw("Vertical");
    }


    private void FixedUpdate()
    {
        Vector3 moveInput = new Vector3(moveX,0,moveZ);
        Vector3 targetVelocity = transform.TransformDirection(moveInput) * moveSpeed;
        Vector3 velocityChange = (targetVelocity - rigid.velocity);

        Move(velocityChange);
    }

    private void Rotation(Vector3 deltaRotation)
    {
        rigid.angularVelocity = deltaRotation;
    }

    private void Move(Vector3 velocityChange)
    {
        rigid.AddForce(velocityChange, ForceMode.VelocityChange);
    }
}