using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Vector2 moveInput;
    private Vector2 moveVelocity;
    private Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        rb.freezeRotation = true;
    }

    void Update()
    {
        moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        moveVelocity = moveInput.normalized * moveSpeed;

        if (moveInput.x > 0) // Jika bergerak ke kanan
        {
            animator.SetBool("isWalkingRight", true);
            animator.SetBool("isWalkingLeft", false);
            animator.SetBool("isWalkingDown", false);
            animator.SetBool("isWalkingUp", false);
        }
        else if (moveInput.x < 0) // Jika bergerak ke kiri
        {
            animator.SetBool("isWalkingRight", false);
            animator.SetBool("isWalkingLeft", true);
            animator.SetBool("isWalkingDown", false);
            animator.SetBool("isWalkingUp", false);
        }
        else if (moveInput.y < 0) // Jika bergerak ke bawah
        {
            animator.SetBool("isWalkingRight", false);
            animator.SetBool("isWalkingLeft", false);
            animator.SetBool("isWalkingDown", true);
            animator.SetBool("isWalkingUp", false);
        }
        else if (moveInput.y > 0) // Jika bergerak ke atas
        {
            animator.SetBool("isWalkingRight", false);
            animator.SetBool("isWalkingLeft", false);
            animator.SetBool("isWalkingDown", false);
            animator.SetBool("isWalkingUp", true);
        }
        else
        {
            animator.SetBool("isWalkingRight", false);
            animator.SetBool("isWalkingLeft", false);
            animator.SetBool("isWalkingDown", false);
            animator.SetBool("isWalkingUp", false);
        }
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveVelocity * Time.fixedDeltaTime);
    }
}
