using UnityEngine;

public class ObjectMovementSound : MonoBehaviour
{
    public AudioSource movementAudioSource; // �Ҹ��� ����� AudioSource
    public AudioClip movementClip; // ����� �Ҹ� Ŭ��
    public float movementThreshold = 0.01f; // �������� ������ �ּ� �Ÿ�

    private Vector3 previousPosition; // ���� �������� ������Ʈ ��ġ

    void Start()
    {
        movementAudioSource.clip = movementClip;
        movementAudioSource.loop = true; // �ݺ� ��� ����
        previousPosition = transform.position; // �ʱ� ��ġ ����
    }

    void Update()
    {
        // ���� ��ġ�� ���� ��ġ�� ���̸� ����Ͽ� ������Ʈ�� �����̴��� Ȯ��
        float distanceMoved = Vector3.Distance(transform.position, previousPosition);

        if (distanceMoved > movementThreshold)
        {
            // �������� �����Ǹ� �Ҹ��� ���
            if (!movementAudioSource.isPlaying)
            {
                movementAudioSource.Play();
            }
        }
        else
        {
            // �������� ������ �Ҹ� ����
            movementAudioSource.Stop();
        }

        // ���� ��ġ�� ���� ��ġ�� ������Ʈ
        previousPosition = transform.position;
    }
}
