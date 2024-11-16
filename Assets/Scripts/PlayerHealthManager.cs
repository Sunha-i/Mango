using UnityEngine;
using UnityEngine.SceneManagement; // 씬 전환을 위한 네임스페이스 추가

public class PlayerHealthManager : MonoBehaviour
{
    private int _healthpoints;

    private EndingDivideCollector itemCollector;

    private void Awake()
    {
        _healthpoints = 30;
    }

    public bool TakeHit()
    {
        _healthpoints -= 10;
        bool isDead = _healthpoints <= 0;
        if (isDead) _Die();
        return isDead;
    }

    private void _Die()
    {
        Debug.Log("Player Died ~ !");

        itemCollector = GetComponent<EndingDivideCollector>();
        itemCollector?.OnDoorClicked();
    }
}