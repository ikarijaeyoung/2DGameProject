using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private float jumpForce = 10f;
    private Rigidbody2D rb;
    public float inputX;
    private bool isGrounded;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator.SetBool("IsIdle", true);
    }
    private void Update()
    {
        PlayerInput();
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
        Move();
        if (inputX != 0)
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
    private void PlayerInput()
    {
        inputX = Input.GetAxis("Horizontal");
    }

    private void Move()
    {
        rb.velocity = new Vector2(inputX * speed, rb.velocity.y);

        if (inputX < 0)
        {
            spriteRenderer.flipX = true;

        }
        else if (inputX > 0)
        {
            spriteRenderer.flipX = false;
        }
        // 위와 아래 코드 동일
        // _spriteRenderer.flipX = inputX < 0; // 왼쪽으로 이동하면 스프라이트 반전
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
