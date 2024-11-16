using BehaviorTree;
using UnityEngine;

public class CheckIfTargetOutOfRange : Node
{
    private Transform _transform;
    private float _outOfRangeDistance;

    public CheckIfTargetOutOfRange(Transform transform, float outOfRangeDistance)
    {
        _transform = transform;
        _outOfRangeDistance = outOfRangeDistance;
    }

    public override NodeState Evaluate()
    {
        object t = GetData("target");
        if (t == null)
        {
            state = NodeState.FAILURE;
            return state;
        }

        Transform target = t as Transform;
        if (target != null)
        {
            float distanceToTarget = Vector3.Distance(_transform.position, target.position);
            if (distanceToTarget > _outOfRangeDistance)
            {
                ClearData("target");
                state = NodeState.FAILURE;
                return state;
            }
        }

        state = NodeState.SUCCESS;
        return state;
    }
}
