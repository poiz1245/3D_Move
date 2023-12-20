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

    float horizontalInput;
    float verticaInput;

    void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticaInput = Input.GetAxisRaw("Vertical");
    }


    private void FixedUpdate()
    {
        Vector3 moveDir = new Vector3(horizontalInput, rigid.velocity.y, verticaInput);

        Vector3 targetVelocity = moveDir * moveSpeed;
        Vector3 velocityChange = (targetVelocity - rigid.velocity);

        Movement(velocityChange);
        Rotation(moveDir);
    }

    private void Rotation(Vector3 moveDir)
    {
        if (moveDir.x != 0 || moveDir.z != 0)
        {
            Quaternion deltaRotation = Quaternion.LookRotation(moveDir.normalized);
            rigid.MoveRotation(deltaRotation);
        }
    }

    private void Movement(Vector3 velocityChange)
    {
        rigid.AddForce(velocityChange, ForceMode.VelocityChange);
    }
}