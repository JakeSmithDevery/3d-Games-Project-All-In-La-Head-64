using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[Serializable]
public enum EnemyState
{
    Idle,
    Following,
    Attacking
}
public class enemycontroller : MonoBehaviour
{
    [Header("Debug")]
    [SerializeField] private EnemyState CurrentState = EnemyState.Idle;

    [Header("Required References")]
    
    [SerializeField] private Transform PlayerTransform;
    [SerializeField] private PlayerLocomotion Player;

    [Header("Properties")]
    [SerializeField] private float DetectionRange = 4;
    [SerializeField] private float LoseDetectionRange = 6;
    [SerializeField] private float AttackRange = 2.5f;

    private NavMeshAgent agent;

   

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        SetState(EnemyState.Idle);

        Player = FindFirstObjectByType<PlayerLocomotion>();

        PlayerTransform = Player.transform;
    }

    void Update()
    {
        switch (CurrentState)
        {
            case EnemyState.Idle:
                

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
                    SetState(EnemyState.Idle);
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
                    SetState(EnemyState.Idle);
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
            case EnemyState.Idle:


                if (IsPlayerWithinFollowRange())
                {
                    SetState(EnemyState.Following);
                }
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

   

    private void OnDrawGizmos()
    {
        //Gizmos.color = Color.green;
        //Gizmos.DrawWireSphere(transform.position, DetectionRange);

        //Gizmos.color = Color.red;
        //Gizmos.DrawWireSphere(transform.position, LoseDetectionRange);

        //Gizmos.color = Color.magenta;
        //Gizmos.DrawWireSphere(transform.position, AttackRange);
    }
}
