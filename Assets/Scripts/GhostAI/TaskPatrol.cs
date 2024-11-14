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
        _agent.updateRotation = false;
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
            Vector3 agentPositionXZ = new Vector3(_transform.position.x, 0, _transform.position.z);
            Vector3 waypointPositionXZ = new Vector3(wp.position.x, 0, wp.position.z);
            float distanceToWaypoint = Vector3.Distance(agentPositionXZ, waypointPositionXZ);

            // Move towards the waypoints
            if (!_waiting)
            {
                _agent.isStopped = false;
                _agent.SetDestination(wp.position);
            }

            if (!_agent.pathPending && _agent.hasPath)
            {
                if (_agent.remainingDistance <= _agent.stoppingDistance 
                    || Vector3.Distance(new Vector3(_transform.position.x, 0, _transform.position.z),
                    new Vector3(wp.position.x, 0, wp.position.z)) < 0.05f)
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
                    Vector3 direction = (_agent.steeringTarget - _transform.position).normalized;
                    direction.y = 0;
                    if (_agent.velocity != Vector3.zero)
                    {
                        Debug.DrawRay(_transform.position, -direction * 2.0f, Color.red, 1.0f);

                        Quaternion targetRotation = Quaternion.LookRotation(-direction, Vector3.up);    // Reverse the direction to face the target
                        _transform.rotation = Quaternion.Slerp(_transform.rotation, targetRotation, Time.deltaTime * 5.0f);

                        //_transform.forward = -direction;
                    }

                    _animator.SetBool("Flying", true);
                }
            }
        }

        state = NodeState.RUNNING;
        return state;
    }
}
