using System.Collections;
using UnityEngine;
using BehaviorTree;

public class CheckFlashlightHitsHead : Node
{
    private Transform _transform;
    private Animator _animator;
    private UnityEngine.AI.NavMeshAgent _agent;
    private bool isHit = false;
    private bool isProcessing = false;

    public CheckFlashlightHitsHead(Transform transform, FlashlightController flashlightController)
    {
        _transform = transform;
        _animator = transform.GetComponentInChildren<Animator>();
        _agent = transform.GetComponent<UnityEngine.AI.NavMeshAgent>();

        flashlightController.LightHitEvent += OnFlashlightHit;
    }

    private void OnFlashlightHit(Transform hitTransform)
    {
        if (hitTransform.root == _transform)
        {
            isHit = true;
            Debug.Log($"{hitTransform.root.name} was hit by the flashlight!");
        }
    }

    public override NodeState Evaluate()
    {
        if (isHit && !isProcessing)
        {
            isProcessing = true;
            isHit = false;

            if (_agent != null)
            {
                _agent.speed = 0f;
            }

            _animator.SetTrigger("Hit");
            _transform.GetComponent<MonoBehaviour>().StartCoroutine(PlayIdleAfterHit());
            state = NodeState.SUCCESS;
            return state;
        }

        state = NodeState.FAILURE;
        return state;
    }

    private IEnumerator PlayIdleAfterHit()
    {
        yield return new WaitForSeconds(1f);
        _animator.SetTrigger("Idle");
        yield return new WaitForSeconds(2f);

        if (_agent != null)
        {
            _agent.speed = 3f;
        }

        isProcessing = false;
    }
}
