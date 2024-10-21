using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashlightController : MonoBehaviour
{
    private Light flashlight;

    public int wideOuterAngle = 100;
    public int wideInnerAngle = 0;
    public int narrowOuterAngle = 20;
    public int narrowInnerAngle = 100;

    public float transitionSpeed = 5f;
    private bool isAttackMode = false;
    private float targetOuterAngle;
    private float targetInnerAngle;

    private void Start()
    {
        flashlight = GetComponentInChildren<Light>();

        targetOuterAngle = wideOuterAngle;
        targetInnerAngle = wideInnerAngle;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            ToggleFlashMode();
        }

        flashlight.spotAngle = Mathf.Lerp(flashlight.spotAngle, targetOuterAngle, Time.deltaTime * transitionSpeed);
        flashlight.innerSpotAngle = Mathf.Lerp(flashlight.innerSpotAngle, targetInnerAngle, Time.deltaTime * transitionSpeed);
    }

    void ToggleFlashMode()
    {
        isAttackMode = !isAttackMode;

        if (isAttackMode)
        {
            targetOuterAngle = narrowOuterAngle;
            targetInnerAngle = narrowInnerAngle;
        }
        else
        {
            targetOuterAngle = wideOuterAngle;
            targetInnerAngle = wideInnerAngle;
        }
    }
}