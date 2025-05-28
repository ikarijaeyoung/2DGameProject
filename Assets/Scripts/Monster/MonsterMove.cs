using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MonsterMove : MonoBehaviour
{
    [SerializeField] private int stageLevel = 1;
    private Animator animator;
    [SerializeField] private GameObject target;
    [SerializeField] private float moveSpeed;
    private bool isMoving = true;
    private void Start()
    {
        animator = GetComponent<Animator>();
        target = GameObject.FindGameObjectWithTag("Player");
    }
    private void Update()
    {
        Stage();
        if (isMoving)
        {
            Move();
        }
    }
    private void Stage()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            stageLevel++;
            Debug.Log("Stage Level: " + stageLevel);
        }
    }
    private void Move()
    {
        Vector2 targetPosition = new Vector2(target.transform.position.x, 0f);
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isMoving = false;
        }
    }
}
