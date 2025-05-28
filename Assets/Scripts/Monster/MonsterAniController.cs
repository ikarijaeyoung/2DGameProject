using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAniController : MonoBehaviour
{
    private Animator animator;
    private Coroutine attackCoroutine;
    private bool isAttacking = false;
    [SerializeField] private float attackSpeed = 3f;

    void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetBool("IsIdle", true);
    }

    public void OnPlayerEnterAttackRange(Collider2D playerBodyCollider)
    {
        isAttacking = true;
        if (attackCoroutine == null)
        {
            attackCoroutine = StartCoroutine(AttackCoroutine());
        }
    }

    public void OnPlayerExitAttackRange(Collider2D playerBodyCollider)
    {
        isAttacking = false;
        if (attackCoroutine != null)
        {
            StopCoroutine(attackCoroutine);
            attackCoroutine = null;
        }
        animator.SetBool("IsIdle", true);
    }

    private IEnumerator AttackCoroutine()
    {
        while (isAttacking)
        {
            animator.SetTrigger("Attack");

            yield return new WaitForSeconds(attackSpeed);
        }
    }
}