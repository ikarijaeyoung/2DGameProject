using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMove : MonoBehaviour
{
    [SerializeField] private int stageLevel = 1;
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        Stage();
    }
    private void Stage()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            stageLevel++;
            Debug.Log("Stage Level: " + stageLevel);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            animator.SetBool("IsIdle", false);
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            animator.SetBool("IsIdle", true);
        }
    }
}
