using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Player player;
    private BoxCollider2D attackRangeCollider;
    private Animator animator;
    private Coroutine attackCoroutine;
    private Spawner spawner;
    private AudioSource audioSource;
    [SerializeField] public AudioClip hitSound;
    private bool isAttacking = false;

    void Start()
    {
        player = GetComponent<Player>();
        attackRangeCollider = GetComponentInChildren<BoxCollider2D>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        animator.SetBool("IsRun", true);

    }

    public void OnMonsterEnterPlayerAttackRange(Collider2D monsterBodycollider)
    {
        isAttacking = true;
        if (attackCoroutine == null)
        {
            attackCoroutine = StartCoroutine(AttackCoroutine());
        }
    }

    public void OnMonsterExitPlayerAttackRange(Collider2D monsterBodycollider)
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

    public void DealDamage()
    {
        Collider2D[] hits = Physics2D.OverlapBoxAll
        (attackRangeCollider.bounds.center,attackRangeCollider.bounds.size, 0f, LayerMask.GetMask("MonsterBody"));
        Animator monsterAnimator = GameObject.FindWithTag("Monster").GetComponent<Animator>();
        AudioSource monsterAudioSource = GameObject.FindWithTag("Monster").GetComponent<AudioSource>();

        foreach (Collider2D hit in hits)
        {
            Monster monster = hit.GetComponent<Monster>();
            if (monster != null)
            {
                monster.TakeDamage(player.attackDamage);
                monsterAudioSource.PlayOneShot(hitSound);
                monsterAnimator.SetTrigger("Hit");
                if (monster.curHP <= 0)
                {
                    monsterAnimator.SetTrigger("Die");
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
