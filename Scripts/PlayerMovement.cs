using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;
    public Transform orientation;
    float horizontalInput;
    float verticalInput;
    Vector3 direction;
    Rigidbody rig;

    private void Start()
    {
        rig = GetComponent<Rigidbody>();
        rig.freezeRotation = true;
    }

    private void Update()
    {
        Inputs();

    }
    private void FixedUpdate()
    {
        Movement();
    }

    void Inputs()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
    }

    void Movement()
    {
        direction = orientation.forward * verticalInput + orientation.right * horizontalInput;
        rig.AddForce(direction.normalized * moveSpeed, ForceMode.Force);
        Debug.Log(moveSpeed);
    }
}
