using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Player player;
    private Animator animator;

    private Coroutine attackCoroutine;
    private bool isAttacking = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        player = GetComponent<Player>();
        animator.SetBool("IsRun", true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Monster"))
        {
            isAttacking = true;
            if (attackCoroutine == null)
            {
                attackCoroutine = StartCoroutine(AttackCoroutine());
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Monster"))
        {
            isAttacking = false;
            if (attackCoroutine != null)
            {
                StopCoroutine(attackCoroutine);
                attackCoroutine = null;
            }

            animator.SetBool("IsIdle", false);
            animator.SetBool("IsRun", true);
        }
    }

    private IEnumerator AttackCoroutine()
    {
        while (isAttacking)
        {
            animator.SetBool("IsRun", false);
            animator.SetBool("IsIdle", true);

            animator.SetTrigger("Attack");

            yield return new WaitForSeconds(player.attackSpeed);

        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Monster"))
        {
            BackGroundScroll.isScrolling = false;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Monster"))
        {
            BackGroundScroll.isScrolling = true;
        }
    }
}
