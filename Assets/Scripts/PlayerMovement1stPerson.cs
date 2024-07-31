using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement1stPerson : MonoBehaviour
{
    public CharacterController controller;
    public float speed = 12f;
    public float jump = 4f;
    public float gravity = -9.81f; // 높이면 floaty action
    public Transform groundCheck; //바닥과 캐릭터가 맞닿아있는지 확인해줌
    public float groundDistance = 0.2f; //충돌 확인할 영역
    public LayerMask groundMask; //collision 확인

    private Vector3 velocity;
    private bool isGrounded; //캐릭터가 vertical motion이 있는지 없는지 확인
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Gravity and Jump
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask); //true일 때

        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        if (Input.GetButtonDown("Jump") && isGrounded) //땅에 붙어 있고 Jump 버튼이 눌렸을 때
        {
            velocity.y = Mathf.Sqrt(jump * -2.0f * gravity); 
        }

        velocity.y += gravity * Time.deltaTime; //if character is not colliding with anything(in the air)
        controller.Move(velocity*Time.deltaTime);

        //Movement
        float moveLR = Input.GetAxis("Horizontal"); 
        float moveFB = Input.GetAxis("Vertical"); //store wasd input

        Vector3 move = transform.right * moveLR + transform.forward * moveFB;

        controller.Move(move * speed * Time.deltaTime);
    }
}
