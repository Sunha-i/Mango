using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLook1stPerson : MonoBehaviour
{
    public Transform playerRef;
    public float mouseSensitivity = 5f;
    float xRotation = 0f; //vertical motion

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        xRotation -= mouseY; //vertical motion은 mouseY input으로 사용(저장) -> camera가 사용할 수 있도록 local rotation으로 변경
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); //vertical rotation 제한
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerRef.Rotate(Vector3.up * mouseX);
    }
}
