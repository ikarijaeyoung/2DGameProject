using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    [SerializeField] public float attackSpeed = 3f;
    public int maxHP = 10;
    public int curHP;
    public int attackDamage = 1;
    void Start()
    {
        curHP = maxHP;
    }
    public void TakeDamage(int damage)
    {
        curHP -= damage;
        if (curHP <= 0)
        {
            Die();
        }
    }
    public void Die()
    {
        Debug.Log("Monster : 죽음.");
    }
}
