using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

using BehaviorTree;

public class TaskPatrol : Node
{
    private Transform _transform;
    private Animator _animator;
    private NavMeshAgent _agent;
    private Transform[] _waypoints;

    private int _currentWaypointIndex = 0;

    private float _waitTIme = 4f;
    private float _waitCounter = 0f;
    private bool _waiting = false;

    public TaskPatrol(Transform transform, Transform[] waypoints)
    {
        _transform = transform;
        _waypoints = waypoints;
        _animator = transform.GetComponentInChildren<Animator>();
        _agent = transform.GetComponent<NavMeshAgent>();
    }

    public override NodeState Evaluate()
    {
        if (_waiting)
        {
            _waitCounter += Time.deltaTime;
            if (_waitCounter >= _waitTIme)
            {
                _waiting = false;
                _animator.SetBool("Flying", true);
            }
        }
        else
        {
            Transform wp = _waypoints[_currentWaypointIndex];
            if (Vector3.Distance(_transform.position, wp.position) < 0.05f)
            {
                _agent.isStopped = true;
                _waitCounter = 0f;
                _waiting = true;

                _currentWaypointIndex = (_currentWaypointIndex + 1) % _waypoints.Length;
                _animator.SetBool("Flying", false);
            }
            else
            {
                // Calculate direction using agent velocity
                Vector3 direction = _agent.velocity.normalized;
                direction.y = 0;
                if (_agent.velocity != Vector3.zero)
                {
                    Quaternion targetRotation = Quaternion.LookRotation(-direction);    // Reverse the direction to face the target
                    _transform.rotation = Quaternion.Slerp(_transform.rotation, targetRotation, Time.deltaTime * 5.0f);
                }

                // Move towards the waypoints
                _agent.isStopped = false;
                _agent.SetDestination(wp.position);
                
                _animator.SetBool("Flying", true);
            }
        }

        state = NodeState.RUNNING;
        return state;
    }
}
