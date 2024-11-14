using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

public class TaskAttack : Node
{
    private Animator _animator;
    private PlayerHealthManager _playerHpManager;

    private float _attackTime = 1f;
    private float _attackCounter = 0f;

    public TaskAttack(Transform transform)
    {
        _animator = transform.GetComponentInChildren<Animator>();
    }

    public override NodeState Evaluate()
    {
        Transform target = (Transform)GetData("target");
        if (target != null && _playerHpManager == null)
        {
            _playerHpManager = target.GetComponent<PlayerHealthManager>();
        }

        _attackCounter += Time.deltaTime;
        if (_attackCounter >= _attackTime)
        {
            bool enemyIsDead = _playerHpManager.TakeHit();
            if (enemyIsDead)
            {
                ClearData("target");
                _animator.SetBool("Attacking", false);
                _animator.SetBool("Flying", true);

                state = NodeState.SUCCESS;
                return state;
            }
            else
            {
                _attackCounter = 0f;
            }
        }

        state = NodeState.RUNNING;
        return state;
    }

}