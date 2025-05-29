using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAniController : MonoBehaviour
{
    private Monster monster;
    private BoxCollider2D attackRangeCollider;
    private Animator animator;
    private Coroutine attackCoroutine;
    private bool isAttacking = false;
    void Start()
    {
        monster = GetComponent<Monster>();
        attackRangeCollider = GetComponentInChildren<BoxCollider2D>();
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

            yield return new WaitForSeconds(monster.attackSpeed);
        }
    }
    public void DealDamage()
    {
        Collider2D[] hits = Physics2D.OverlapBoxAll
        (attackRangeCollider.bounds.center, attackRangeCollider.bounds.size, 0f, LayerMask.GetMask("PlayerBody"));
        Animator playerAnimator = GameObject.FindWithTag("Player").GetComponent<Animator>();
        foreach (Collider2D hit in hits)
        {
            Player player = hit.GetComponent<Player>();
            if (player != null)
            {
                player.TakeDamage(monster.attackDamage);
                playerAnimator.SetTrigger("Hit");

                if (player.curHP <= 0)
                {
                    playerAnimator.SetTrigger("Die");
                    isAttacking = false;
                    if (attackCoroutine != null)
                    {
                        StopCoroutine(attackCoroutine);
                        attackCoroutine = null;
                    }
                }

            }
        }
    }
    
}