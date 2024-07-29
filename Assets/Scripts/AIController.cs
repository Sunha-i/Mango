using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIController : MonoBehaviour
{
    public NavMeshAgent navMeshAgent;
    public float startWaitTime = 2f;
    public float timeToRotate = 2f;
    public float speedWalk = 1f;
    public float speedRun = 3f;

    public float viewRadius = 15f;
    public float viewAngle = 90f;
    public LayerMask playerMask;
    public LayerMask obstacleMask;
    public float meshResolution = 1f;
    public int edgeIterations = 4;
    public float edgeDistance = 0.5f;

    public Transform[] waypoints;
    int mCurrentWaypointIndex;

    Vector3 playerLastPosition = Vector3.zero;
    Vector3 mPlayerPosition;

    float mWaitTime;
    float mTimeToRotate;
    bool mPlayerInRange;
    bool mPlayerNear;
    bool mIsPatrol;
    bool mCaughtPlayer;

    // Start is called before the first frame update
    void Start()
    {
        mPlayerPosition = Vector3.zero;
        mIsPatrol = true;
        mCaughtPlayer = false;
        mPlayerInRange = false;
        mWaitTime = startWaitTime;
        mTimeToRotate = timeToRotate;

        mCurrentWaypointIndex = 0;
        navMeshAgent = GetComponent<NavMeshAgent>();

        navMeshAgent.isStopped = false;
        navMeshAgent.speed = speedWalk;
        navMeshAgent.SetDestination(waypoints[mCurrentWaypointIndex].position);
    }

    // Update is called once per frame
    void Update()
    {
        EnvironmentView();

        if (!mIsPatrol)
        {
            Chasing();
        }
        else
        {
            Patrolling();
        }
    }

    private void Chasing()
    {
        mPlayerNear = false;
        playerLastPosition = Vector3.zero;

        if (!mCaughtPlayer)
        {
            Move(speedRun);
            navMeshAgent.SetDestination(mPlayerPosition);
        }
        if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
        {
            if (mWaitTime <= 0 && !mCaughtPlayer && Vector3.Distance(transform.position, GameObject.FindGameObjectWithTag("Player").transform.position) >= 6f)
            {
                mIsPatrol = true;
                mPlayerNear = false;
                Move(speedWalk);
                mTimeToRotate = timeToRotate;
                mWaitTime = startWaitTime;
                navMeshAgent.SetDestination(waypoints[mCurrentWaypointIndex].position);
            }
            else
            {
                if (Vector3.Distance(transform.position, GameObject.FindGameObjectWithTag("Player").transform.position) >= 2.5f)
                {
                    Stop();
                    mWaitTime -= Time.deltaTime;
                }
            }
        }
    }

    private void Patrolling()
    {
        if (mPlayerNear)
        {
            if (mTimeToRotate <= 0)
            {
                Move(speedWalk);
                LookingPlayer(playerLastPosition);
            }
            else
            {
                Stop();
                mTimeToRotate -= Time.deltaTime;
            }
        }
        else
        {
            mPlayerNear = false;
            playerLastPosition = Vector3.zero;
            navMeshAgent.SetDestination(waypoints[mCurrentWaypointIndex].position);
            
            if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
            {
                if (mWaitTime <= 0)
                {
                    NextPoint();
                    Move(speedWalk);
                    mWaitTime = startWaitTime;
                }
                else
                {
                    Stop();
                    mWaitTime -= Time.deltaTime;
                }
            }
        }
    }

    void Move(float speed)
    {
        navMeshAgent.isStopped = false;
        navMeshAgent.speed = speed;
    }
    void Stop()
    {
        navMeshAgent.isStopped = true;
        navMeshAgent.speed = 0;
    }

    public void NextPoint()
    {
        mCurrentWaypointIndex = (mCurrentWaypointIndex + 1) % waypoints.Length;
        navMeshAgent.SetDestination(waypoints[mCurrentWaypointIndex].position);
    }

    void CaughtPlayer()
    {
        mCaughtPlayer = true;
    }

    void LookingPlayer(Vector3 player)
    {
        navMeshAgent.SetDestination(player);
        if (Vector3.Distance(transform.position, player) <= 0.3f)
        {
            if (mWaitTime <= 0)
            {
                mPlayerNear = false;
                Move(speedWalk);
                navMeshAgent.SetDestination(waypoints[mCurrentWaypointIndex].position);
                mWaitTime = startWaitTime;
                mTimeToRotate = timeToRotate;
            }
            else
            {
                Stop();
                mWaitTime -= Time.deltaTime;
            }
        }
    }

    void EnvironmentView()
    {
        Collider[] playerInRange = Physics.OverlapSphere(transform.position, viewRadius, playerMask);

        for (int i = 0; i < playerInRange.Length; i++)
        {
            Transform player = playerInRange[i].transform;
            Vector3 dirToPlayer = (player.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, dirToPlayer) < viewAngle / 2)
            {
                float dstToPlayer = Vector3.Distance(transform.position, player.position);
                if (!Physics.Raycast(transform.position, dirToPlayer, dstToPlayer, obstacleMask))
                {
                    mPlayerInRange = true;
                    mIsPatrol = false;
                }
                else
                {
                    mPlayerInRange = false;
                }
            }
            if (Vector3.Distance(transform.position, player.position) > viewRadius)
            {
                mPlayerInRange = false;
            }
            if (mPlayerInRange)
            {
                mPlayerPosition = player.transform.position;
            }
        }
    }
}
