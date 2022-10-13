using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public CharacterController controller;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    public float speed = 12f;
    public float gravity = -9.81f;
    public float jumpHeight = 4f;
    Vector3 velocity;
    bool isGrounded;
    
    void Update()
    {
        //groundCheck etrafında bir küre oluşturur. Mask ile collide oluyor mu kontrol eder.
        isGrounded = Physics.CheckSphere(groundCheck.position,groundDistance,groundMask);

        //yerçekimi kontrol
        if(isGrounded && velocity.y < 0) {
            velocity.y = -2f;
        }

    /// transform. kullanmamızın sebebi karakterin baktığı yöne doğru ilerlemesini istememiz.
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed *  Time.deltaTime );

        if(Input.GetButtonDown("Jump") && isGrounded){
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        //Gravity
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}
