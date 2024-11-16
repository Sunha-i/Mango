using UnityEngine;

public class PlayerFootstep : MonoBehaviour
{
    public AudioSource footstepAudioSource; // ���ڱ� �Ҹ� AudioSource
    public AudioClip footstepClip; // ���ڱ� �Ҹ� Ŭ��
    public float stepInterval = 0.5f; // �Ҹ� ���� (�� ����)

    private float stepTimer; // �Ҹ� ��� ���� Ÿ�̸�

    void Start()
    {
        footstepAudioSource.clip = footstepClip;
        footstepAudioSource.loop = true; // �Ҹ� �ݺ� ��� ����
    }

    void Update()
    {
        // WASD Ű �Է� ����
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            if (!footstepAudioSource.isPlaying)
            {
                footstepAudioSource.Play();
            }
        }
        else
        {
            // Ű�� ���� �Ҹ� ����
            footstepAudioSource.Stop();
        }
    }
}
