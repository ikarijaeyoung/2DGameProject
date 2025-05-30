using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private float jumpForce = 10f;
    private Rigidbody2D rb;
    private Vector2 movement;
    private bool isGrounded;
    private Animator animator;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        animator.SetBool("IsIdle", true);
    }
    private void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        if (Input.GetKeyDown(KeyCode.F) && isGrounded)
        {
            Jump();
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Attack();
        }
    }
    private void FixedUpdate()
    {
        rb.velocity = new Vector2(movement.x * speed, rb.velocity.y);
        if (movement.x != 0)
        {
            animator.SetBool("IsIdle", false);
            animator.SetBool("IsRun", true);
        }
        else
        {
            animator.SetBool("IsIdle", true);
            animator.SetBool("IsRun", false);
        }
    }
    private void Jump()
    {
        rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
        animator.SetTrigger("Jump");
    }
    private void Attack()
    {
        animator.SetTrigger("Attack");
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            isGrounded = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            isGrounded = false;
        }
    }
}
