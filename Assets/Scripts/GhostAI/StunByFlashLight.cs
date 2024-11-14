using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

public class StunByFlashLight : Node
{
    private Transform _transform;
    private Animator _animator;
    private float _waitTIme = 3f;
    private float _waitCounter = 0f;

    public StunByFlashLight(Transform transform)
    {
        _transform = transform;
        _animator = transform.GetComponentInChildren<Animator>();
    }

    public override NodeState Evaluate()
    {
        //if (_waitCounter == 0f)
        //{
        //    _animator.SetTrigger("Hit");
        //}

        _waitCounter += Time.deltaTime;
        if (_waitCounter >= _waitTIme)
        {
            _waitCounter = 0f;
            return NodeState.SUCCESS;
        }

        state = NodeState.RUNNING;
        return state;
    }
}
