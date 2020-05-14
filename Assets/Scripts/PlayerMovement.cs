using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 12f;
    public float groundDistance = 0.4f;
    public float jumpHeight = 4f;
    public float gravity = -9.81f;

    public CharacterController controller;
    public Transform groundCheck;
    public LayerMask groundMask;

    float xAxis, zAxis;
    bool isGrounded;

    Vector3 move;
    Vector3 velocity;
    

    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
            xAxis = Input.GetAxis("Horizontal");
            zAxis = Input.GetAxis("Vertical");
        }

        move = transform.right * xAxis + transform.forward * zAxis;
        controller.Move(move * speed * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}
