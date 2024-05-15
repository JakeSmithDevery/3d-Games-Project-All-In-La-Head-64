using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[Serializable]
public enum EnemyState
{
    Idle,
    Patrolling,
    Following,
    Attacking
}
public class enemycontroller : MonoBehaviour
{
    [Header("Debug")]
    [SerializeField] private EnemyState CurrentState = EnemyState.Idle;

    [Header("Required References")]
    
    [SerializeField] private Transform PlayerTransform;

    [Header("Properties")]
    [SerializeField] private float DetectionRange = 4;
    [SerializeField] private float LoseDetectionRange = 6;
    [SerializeField] private float AttackRange = 2.5f;

    private NavMeshAgent agent;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Start()
    {
        SetState(EnemyState.Patrolling);
    }

    void Update()
    {
        switch (CurrentState)
        {
            case EnemyState.Patrolling:
                if (HasReachedDestination())
                {
                    
                }

                if (IsPlayerWithinFollowRange())
                {
                    SetState(EnemyState.Following);
                }
                break;

            case EnemyState.Following:
                if (IsWithinAttackRange())
                {
                    SetState(EnemyState.Attacking);
                }
                else if (!HasLostPlayer())
                {
                    agent.SetDestination(PlayerTransform.position);
                }
                else if (HasLostPlayer())
                {
                    SetState(EnemyState.Patrolling);
                }
                break;

            case EnemyState.Attacking:
                if (!IsWithinAttackRange() && IsPlayerWithinFollowRange())
                {
                    CancelInvoke("Attack");
                    SetState(EnemyState.Following);
                }
                else if (HasLostPlayer())
                {
                    CancelInvoke("Attack");
                    SetState(EnemyState.Patrolling);
                }
                break;
        }
    }

    public void SetState(EnemyState newState)
    {
        if (CurrentState == newState) return;

        CurrentState = newState;

        switch (CurrentState)
        {

            case EnemyState.Patrolling:
                
                break;

            case EnemyState.Following:
                agent.SetDestination(PlayerTransform.position);
                break;

            case EnemyState.Attacking:
                if (IsInvoking("Attack"))
                {
                    InvokeRepeating("Attack", 2, 2);
                }
                break;
        }
    }

    private void Attack()
    {
        Debug.Log("Attack!");
    }

    private bool IsWithinAttackRange()
    {
        return Vector3.Distance(transform.position, PlayerTransform.position) <= AttackRange;
    }

    private bool HasLostPlayer()
    {
        return Vector3.Distance(transform.position, PlayerTransform.position) >= LoseDetectionRange;
    }

    private bool IsPlayerWithinFollowRange()
    {
        return Vector3.Distance(transform.position, PlayerTransform.position) <= DetectionRange;
    }

    private bool HasReachedDestination()
    {
        return agent.remainingDistance <= agent.stoppingDistance;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, DetectionRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, LoseDetectionRange);

        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, AttackRange);
    }
}
