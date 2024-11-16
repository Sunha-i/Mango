using UnityEngine;

public class PlayerFootstep : MonoBehaviour
{
    public AudioSource footstepAudioSource; // 발자국 소리 AudioSource
    public AudioClip footstepClip; // 발자국 소리 클립
    public float stepInterval = 0.5f; // 소리 간격 (초 단위)

    private float stepTimer; // 소리 재생 간격 타이머

    void Start()
    {
        footstepAudioSource.clip = footstepClip;
        footstepAudioSource.loop = true; // 소리 반복 재생 설정
    }

    void Update()
    {
        // WASD 키 입력 감지
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            if (!footstepAudioSource.isPlaying)
            {
                footstepAudioSource.Play();
            }
        }
        else
        {
            // 키를 떼면 소리 멈춤
            footstepAudioSource.Stop();
        }
    }
}
