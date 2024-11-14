using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

public class CheckEnemyInAttackRange : Node
{
    private static int _enemyLayerMask = 1 << 3;

    private Transform _transform;
    private Animator _animator;

    public CheckEnemyInAttackRange(Transform transform)
    {
        _transform = transform;
        _animator = transform.GetComponentInChildren<Animator>();
    }

    public override NodeState Evaluate()
    {
        object t = parent.parent.GetData("target");
        if (t == null)
        {
            state = NodeState.FAILURE;
            return state;
        }

        Transform target = (Transform)t;
        Vector3 targetPosition = new Vector3(target.position.x, _transform.position.y, target.position.z);
        if (Vector3.Distance(_transform.position, targetPosition) <= GhostBT.attackRange)
        {
            _animator.SetBool("Attacking", true);
            _animator.SetBool("Flying", false);

            state = NodeState.SUCCESS;
            return state;
        }

        state = NodeState.FAILURE;
        return state;
    }
}
