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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("맞았다.");
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Player이다.");
            isAttacking = true;
            if (attackCoroutine == null)
            {
                Debug.Log("공격 시작");
                attackCoroutine = StartCoroutine(AttackCoroutine());
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("나갔다.");
        if (collision.CompareTag("Player"))
        {   
            Debug.Log("Player가 나갔다.");
            isAttacking = false;
            if (attackCoroutine != null)
            {
                Debug.Log("공격 중지");
                StopCoroutine(attackCoroutine);
                attackCoroutine = null;
            }
            animator.SetBool("IsIdle", true);
        }
    }

    private IEnumerator AttackCoroutine()
    {
        while (isAttacking)
        {
            animator.SetBool("IsIdle", true);
            animator.SetTrigger("Attack");

            yield return new WaitForSeconds(attackSpeed);

        }
    }
}
