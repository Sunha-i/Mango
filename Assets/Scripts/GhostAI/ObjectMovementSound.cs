using UnityEngine;

public class ObjectMovementSound : MonoBehaviour
{
    public AudioSource movementAudioSource; // 소리를 재생할 AudioSource
    public AudioClip movementClip; // 재생할 소리 클립
    public float movementThreshold = 0.01f; // 움직임을 감지할 최소 거리

    private Vector3 previousPosition; // 이전 프레임의 오브젝트 위치

    void Start()
    {
        movementAudioSource.clip = movementClip;
        movementAudioSource.loop = true; // 반복 재생 설정
        previousPosition = transform.position; // 초기 위치 설정
    }

    void Update()
    {
        // 현재 위치와 이전 위치의 차이를 계산하여 오브젝트가 움직이는지 확인
        float distanceMoved = Vector3.Distance(transform.position, previousPosition);

        if (distanceMoved > movementThreshold)
        {
            // 움직임이 감지되면 소리를 재생
            if (!movementAudioSource.isPlaying)
            {
                movementAudioSource.Play();
            }
        }
        else
        {
            // 움직임이 없으면 소리 멈춤
            movementAudioSource.Stop();
        }

        // 현재 위치를 이전 위치로 업데이트
        previousPosition = transform.position;
    }
}
