using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    public Transform _camera;
    public Transform hand;
    public float cameraSensitivity = 15.0f;
    public float cameraAcceleration = 5.0f;

    private float rotation_x_axis;
    private float rotation_y_axis;

    private PlayerInputActions playerControls;

    void Awake()
    {
        playerControls = new PlayerInputActions();
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        Vector2 look = playerControls.Player.Look.ReadValue<Vector2>();

        rotation_x_axis += look.y * cameraSensitivity * Time.deltaTime;
        rotation_y_axis += look.x * cameraSensitivity * Time.deltaTime;

        rotation_x_axis = Mathf.Clamp(rotation_x_axis, -90.0f, 90.0f);

        hand.localRotation = Quaternion.Euler(-rotation_x_axis, rotation_y_axis, 0);

        transform.localRotation = Quaternion.Lerp(transform.localRotation,
            Quaternion.Euler(0, rotation_y_axis, 0), cameraAcceleration * Time.deltaTime);
        _camera.localRotation = Quaternion.Lerp(_camera.localRotation,
            Quaternion.Euler(-rotation_x_axis, 0, 0), cameraAcceleration * Time.deltaTime);
    }

    public void OnEnable()
    {
        playerControls.Enable();
    }

    public void OnDisable()
    {
        playerControls.Disable();
    }
}
