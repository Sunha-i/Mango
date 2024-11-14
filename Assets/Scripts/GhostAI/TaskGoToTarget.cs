using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

using BehaviorTree;

public class TaskGoToTarget : Node
{
    private Transform _transform;
    private NavMeshAgent _agent;
    private float _stopDistance = 1.5f;

    public TaskGoToTarget(Transform transform)
    {
        _transform = transform;
        _agent = transform.GetComponent<NavMeshAgent>();
        _agent.stoppingDistance = _stopDistance;
    }

    public override NodeState Evaluate()
    {
        Transform target = (Transform)GetData("target");

        Vector3 targetPosition = new Vector3(target.position.x, _transform.position.y, target.position.z);
        float stopDistance = 1f;

        float distanceToTarget = Vector3.Distance(_transform.position, targetPosition);
        if (distanceToTarget > _stopDistance)
        {
            Vector3 direction = (targetPosition - _transform.position).normalized;
            direction.y = 0;
            Quaternion targetRotation = Quaternion.LookRotation(-direction);    // Reverse the direction to face the target
            _transform.rotation = Quaternion.Slerp(_transform.rotation, targetRotation, Time.deltaTime * 5.0f);

            _agent.SetDestination(targetPosition);

            state = NodeState.RUNNING;
        }
        else
        {
            state = NodeState.SUCCESS;
        }

        return state;
    }
}
