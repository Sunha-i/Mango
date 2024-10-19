using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using Cursor = UnityEngine.Cursor;

public class ObjectInteraction : MonoBehaviour
{
    public float playerReach = 3f;
    Examinable currentInteractable;

    public GameObject offset;
    public bool isExamining = false;
    public bool isInteracting = false;
    private Vector3 lastMousePosition;
    private Transform examinedObject; // Store the currently examined object

    //List of position and rotation of the interactble objects 
    private Dictionary<Transform, Vector3> originalPositions = new Dictionary<Transform, Vector3>();
    private Dictionary<Transform, Quaternion> originalRotations = new Dictionary<Transform, Quaternion>();

    private MovementController _moveControls;
    private CameraController _cameraControls;
    public Canvas _canvas;

    void Start()
    {
        _moveControls = GetComponent<MovementController>();
        _cameraControls = GetComponentInChildren<CameraController>();
    }

    void Update()
    {
        CheckInteraction();
        if (Input.GetKeyDown(KeyCode.E) && currentInteractable != null)
        {
            string objectTag = currentInteractable.gameObject.tag;
            if (objectTag == "Examinable")
            {
                //currentInteractable.Pickup();
                ToggleExamination();
            }
            else if (objectTag == "Interactable")
            {
                ToggleInteraction();
            }

            // Store the currently examined object and its original position and rotation
            if (isExamining)
            {
                examinedObject = currentInteractable.transform;
                originalPositions[examinedObject] = examinedObject.position;
                originalRotations[examinedObject] = examinedObject.rotation;
            }
            if (isInteracting)
            {
                examinedObject = currentInteractable.transform;
                originalPositions[examinedObject] = examinedObject.position;
                originalRotations[examinedObject] = examinedObject.rotation;
            }
        }
        if (isExamining)
        {
            _canvas.enabled = false;
            Examine(); StartExamination();
        }
        else
        {
            _canvas.enabled = true;
            NonExamine(); StopExamination();
        }

        if (Input.GetKeyDown(KeyCode.F) && currentInteractable != null)
        {
            string objectTag = currentInteractable.gameObject.tag;
            if (objectTag == "Pickable")
            {
                currentInteractable.RealPickup();
            }
        }
    }

    void CheckInteraction()
    {
        RaycastHit hit;
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);

        // if colliders with anything within player reach
        if (Physics.Raycast(ray, out hit, playerReach))
        {
            string[] validTags = { "Examinable", "Interactable", "Pickable" };

            if (validTags.Contains(hit.collider.tag)) // if looking at an interactable object
            {
                Examinable newInteractable = hit.collider.GetComponent<Examinable>();

                // if there is a currentInteractable and it is not the newInteractable
                if (currentInteractable && newInteractable != currentInteractable)
                {
                    currentInteractable.DisableOutline();
                }
                if (newInteractable.enabled)
                {
                    SetNewCurrentInteractable(newInteractable);
                }
                else // if new interactable is not enabled
                {
                    DisableCurrentInteractable();
                }
            }
            else // if not an interactable
            {
                DisableCurrentInteractable();
            }
        }
        else // if nothing in reach
        {
            DisableCurrentInteractable();
        }
    }

    void SetNewCurrentInteractable(Examinable newInteractable)
    {
        currentInteractable = newInteractable;
        currentInteractable.EnableOutline();
        HUDController.instance.EnableExaminationText(currentInteractable.message);
    }

    void DisableCurrentInteractable()
    {
        HUDController.instance.DisableExaminableText();
        if (currentInteractable)
        {
            currentInteractable.DisableOutline();
            currentInteractable = null;
        }
    }

    public void ToggleExamination()
    {
        isExamining = !isExamining;
    }

    public void ToggleInteraction()
    {
        isInteracting = !isInteracting;
    }

    // This method is called when the player is examining an object.
    // It moves the examined object towards the offset object and allows the player to rotate it based on mouse movement.

    void Examine()
    {
        if (examinedObject != null)
        {
            examinedObject.position = Vector3.Lerp(examinedObject.position, offset.transform.position, 0.2f);

            Vector3 deltaMouse = Input.mousePosition - lastMousePosition;
            float rotationSpeed = 1.0f;
            examinedObject.Rotate(deltaMouse.x * rotationSpeed * Vector3.up, Space.World);
            examinedObject.Rotate(deltaMouse.y * rotationSpeed * Vector3.left, Space.World);
            lastMousePosition = Input.mousePosition;
        }
    }

    //This method is called when the player is not examining an object.
    //It resets the position and rotation of the examined object to its original values stored in the dictionaries.

    void NonExamine()
    {
        if (examinedObject != null)
        {
            // Reset the position and rotation of the examined object to its original values
            if (originalPositions.ContainsKey(examinedObject))
            {
                examinedObject.position = Vector3.Lerp(examinedObject.position, originalPositions[examinedObject], 0.2f);
            }
            if (originalRotations.ContainsKey(examinedObject))
            {
                examinedObject.rotation = Quaternion.Slerp(examinedObject.rotation, originalRotations[examinedObject], 0.2f);
            }
        }
    }

    // This method is called when the player starts examining an object. It locks the cursor,
    // makes it visible, and disables the PlayerInput component to prevent player movement during examination.

    void StartExamination()
    {
        lastMousePosition = Input.mousePosition;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        _moveControls.OnDisable();
        _cameraControls.OnDisable();
    }

    //This method is called when the player stops examining an object. It locks the cursor again,
    //hides it, and re-enables the PlayerInput component to allow player movement.

    void StopExamination()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        _moveControls.OnEnable();
        _cameraControls.OnEnable();
    }
}