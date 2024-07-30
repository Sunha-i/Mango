using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement3rdPerson : MonoBehaviour
{
    public CharacterController controller;
    public Transform cam;
    public Transform groundCheck; //바닥과 캐릭터가 맞닿아있는지 확인해줌
    public LayerMask groundMask; //collision 확인
    public float speed = 6f;
    public float jump = 3f;
    public float turnSmoothTime = 0.1f;
    public float gravity = -9.81f; // 높이면 floaty action
    public float groundDistance = 0.2f; //충돌 확인할 영역

    private float turnSmoothVelocity;
    private Vector3 velocity;
    private bool isGrounded; //캐릭터가 vertical motion이 있는지 없는지 확인

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask); //true일 때

        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float moveLR = Input.GetAxisRaw("Horizontal");
        float moveFB = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(moveLR,0f,moveFB).normalized;

        if (direction.magnitude > 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            
            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir * speed * Time.deltaTime);
        }

        if (Input.GetButtonDown("Jump") && isGrounded) //땅에 붙어 있고 Jump 버튼이 눌렸을 때
        {
            velocity.y = Mathf.Sqrt(jump * -2.0f * gravity); 
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity*Time.deltaTime);
    }
}
