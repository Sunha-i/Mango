using System.Collections.Generic;
using BehaviorTree;

public class GhostBT : Tree
{
    public UnityEngine.Transform[] waypoints;
    public FlashlightController flashlightController;

    public static float speed = 3f;
    public static float fovRange = 6f;
    public static float attackRange = 1f;

    protected override Node SetupTree()
    {
        Node root = new Selector(new List<Node>
        {
            new Sequence(new List<Node>
            {
                new CheckFlashlightHitsHead(transform, flashlightController),
                new StunByFlashLight(transform),
            }),
            new Sequence(new List<Node>
            {
                new CheckEnemyInAttackRange(transform),
                new TaskAttack(transform),
            }),
            new Sequence(new List<Node>
            {
                new CheckEnemyInFOVRange(transform),
                new CheckIfTargetOutOfRange(transform, fovRange),
                new TaskGoToTarget(transform),
            }),
            new TaskPatrol(transform, waypoints),
        });

        return root;
    }
}