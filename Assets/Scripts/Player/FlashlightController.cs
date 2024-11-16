using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashlightController : MonoBehaviour
{
    private Light flashlight;
    public LayerMask ghostLayer;
    public float playerReach = 15f;

    public int wideOuterAngle = 100;
    public int wideInnerAngle = 0;
    public int narrowOuterAngle = 20;
    public int narrowInnerAngle = 100;
    public int attackOuterAngle = 50;

    public float transitionSpeed = 5f;
    public float attackEffectDuration = 0.3f;
    public float cooldownTime = 2f;

    private bool isAttackMode = false;
    private bool isCooldown = false;
    private float targetOuterAngle;
    private float targetInnerAngle;

    public event Action<Transform> LightHitEvent;

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

        if (isAttackMode && !isCooldown)
        {
            CheckForGhostHit();
        }
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

    void CheckForGhostHit()
    {
        RaycastHit hit;
        Ray ray = new Ray(flashlight.transform.position, flashlight.transform.forward);

        Debug.DrawRay(flashlight.transform.position, flashlight.transform.forward * playerReach, Color.red, 0.1f);

        if (Physics.Raycast(ray, out hit, playerReach))
        {
            if (hit.collider.CompareTag("GhostTargetPoint"))
            {
                Debug.Log("Ghost hit by flashlight!");

                LightHitEvent?.Invoke(hit.collider.transform);
                StartCoroutine(AttackEffectCoroutine());
            }
        }
    }

    IEnumerator AttackEffectCoroutine()
    {
        isCooldown = true;

        targetOuterAngle = attackOuterAngle;
        yield return new WaitForSeconds(attackEffectDuration);

        targetOuterAngle = narrowOuterAngle;
        yield return new WaitForSeconds(attackEffectDuration);

        targetOuterAngle = wideOuterAngle;
        targetInnerAngle = wideInnerAngle;
        isAttackMode = false;

        yield return new WaitForSeconds(cooldownTime);

        isCooldown = false;
    }
}