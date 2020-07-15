using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Player targetAttack = null;
    [SerializeField] private float distanceToAttack = 1.0f;
    [SerializeField] private float health = 1000.0f;
    [SerializeField] private float armor = 0.0f;
    [SerializeField] private float attack = 50.0f;
    private bool isDead = false;
    private Animator _animator = null;
    private Collider _collider = null;
    private NavMeshAgent _navMeshAgent = null;  

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _collider = GetComponent<Collider>();
    }

    private void Start()
    {
        targetAttack = FindObjectOfType<Player>();
    }

    private void Update()
    {
        if (isDead == false)
        {
            if (Vector3.Distance(transform.position, targetAttack.transform.position) < distanceToAttack)
            {
                _animator.SetBool("Attack", true);
                _navMeshAgent.SetDestination(transform.position);
            }
            else
            {
                _animator.SetBool("Attack", false);
                _navMeshAgent.SetDestination(targetAttack.transform.position);

            }
        }       
    }

    public virtual void TakeDamage(float damage)
    {
        float changeHP = damage - armor;
        if (changeHP > 0 && isDead == false)
        {
            health -= changeHP;

            if (health <= 0)
            {
                health = 0;
                isDead = true;
                Dead();
            }
        }
    }

    private void Dead()
    {
        _navMeshAgent.isStopped = true;
        _animator.SetBool("Dead",true);
        _collider.isTrigger = true;
        Messenger.Broadcast(GameEvent.ENEMY_KILL);
    }

    //Call from Animation Event «Z_Attack»
    public void Attack()
    {
        if (targetAttack != null)
        {
            targetAttack.TakeDamage(attack);
        }
    }
}
