using BehaviorTree;
using UnityEngine;

public class CheckEnemyInFOVRange : Node
{
    private static int _enemyLayerMask = 1 << 3;

    private Transform _transform;
    private Animator _animator;

    public CheckEnemyInFOVRange(Transform transform)
    {
        _transform = transform;
        _animator = transform.GetComponentInChildren<Animator>();
    }

    public override NodeState Evaluate()
    {
        object t = GetData("target");
        if (t == null)
        {
            Collider[] colliders = Physics.OverlapSphere(
                _transform.position, GhostBT.fovRange, _enemyLayerMask);

            if (colliders.Length > 0)
            {
                parent.parent.SetData("target", colliders[0].transform);
                _animator.SetBool("Flying", true);
                state = NodeState.SUCCESS;
                return state;
            }

            state = NodeState.FAILURE;
            return state;
        }

        //Transform target = t as Transform;
        //float distanceToTarget = Vector3.Distance(_transform.position, target.position);
        //if (distanceToTarget > GhostBT.fovRange * 1.5f)
        //{
        //    ClearData("target");
        //    _animator.SetBool("Flying", false);
        //    state = NodeState.FAILURE;
        //    return state;
        //}

        state = NodeState.SUCCESS;
        return state;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_transform.position, GhostBT.fovRange);
    }
}
 