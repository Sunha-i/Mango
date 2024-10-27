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
    public float attackEffectDuration = 0.3f; // 공격 판정 발생 시 outer angle 유지 시간
    public float cooldownTime = 2f; // 쿨다운 시간

    private bool isAttackMode = false;
    private bool isCooldown = false;
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

        if (isAttackMode)
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

                StartCoroutine(AttackEffectCoroutine());
            }
        }
    }

    IEnumerator AttackEffectCoroutine()
    {
        isCooldown = true; // 쿨다운 시작

        for (int i = 0; i < 2; i++) // 두 번 깜박임 효과를 반복
        {
            targetOuterAngle = attackOuterAngle; // outer angle을 넓게 설정
            yield return new WaitForSeconds(attackEffectDuration); // 공격 효과 유지 시간 대기

            targetOuterAngle = narrowOuterAngle; // 다시 원래 크기로 복구
            yield return new WaitForSeconds(attackEffectDuration); // 공격 효과 유지 시간 대기
        }

        yield return new WaitForSeconds(cooldownTime); // 쿨다운 시간 대기

        isCooldown = false; // 쿨다운 해제
    }
}