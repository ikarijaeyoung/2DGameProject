using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackRangeDetector : MonoBehaviour
{
    private PlayerAnimation playerAnimation;
    [SerializeField] private LayerMask monsterLayer;
    private void Start()
    {
        playerAnimation = GetComponentInParent<PlayerAnimation>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (((1 << collision.gameObject.layer) & monsterLayer) != 0)
        {
            playerAnimation.OnMonsterEnterPlayerAttackRange(collision);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (((1 << collision.gameObject.layer) & monsterLayer) != 0)
        {
            playerAnimation.OnMonsterExitPlayerAttackRange(collision);
        }
    }   
}
