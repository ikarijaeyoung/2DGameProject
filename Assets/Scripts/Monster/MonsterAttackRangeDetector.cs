using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAttackRangeDetector : MonoBehaviour
{
    private MonsterAniController monsterAniController;
    [SerializeField] private LayerMask playerBodyLayer;

    void Start()
    {
        monsterAniController = GetComponentInParent<MonsterAniController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (((1 << collision.gameObject.layer) & playerBodyLayer) != 0)
        {
            monsterAniController.OnPlayerEnterAttackRange(collision);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (((1 << collision.gameObject.layer) & playerBodyLayer) != 0)
        {
            monsterAniController.OnPlayerExitAttackRange(collision);
        }
    }
}
