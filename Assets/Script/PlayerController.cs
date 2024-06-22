using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Vector2 moveInput;
    private Vector2 lastMoveDirection; // Variabel untuk menyimpan arah terakhir
    private Animator animator;
    private bool isAttacking;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = true;
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (!isAttacking)
        {
            moveInput.x = Input.GetAxisRaw("Horizontal");
            moveInput.y = Input.GetAxisRaw("Vertical");

            if (moveInput != Vector2.zero)
            {
                lastMoveDirection = moveInput; // Update arah terakhir saat bergerak
                animator.SetFloat("moveX", moveInput.x);
                animator.SetFloat("moveY", moveInput.y);
                animator.SetBool("isMoving", true);
            }
            else
            {
                animator.SetBool("isMoving", false);
            }
        }

        if (Input.GetKeyDown(KeyCode.Space) && !isAttacking)
        {
            Attack();
        }
    }

    private void FixedUpdate()
    {
        if (moveInput != Vector2.zero && !isAttacking)
        {
            Vector2 newPosition = rb.position + moveInput.normalized * moveSpeed * Time.fixedDeltaTime;
            rb.MovePosition(newPosition);
        }
    }

    private void Attack()
    {
        isAttacking = true;
        animator.SetBool("isAttacking", true);

        // Set arah berdasarkan nilai terakhir
        if (moveInput == Vector2.zero)
        {
            animator.SetFloat("moveX", lastMoveDirection.x);
            animator.SetFloat("moveY", lastMoveDirection.y);
        }
        else
        {
            animator.SetFloat("moveX", moveInput.x);
            animator.SetFloat("moveY", moveInput.y);
        }
        
        Debug.Log($"Attack started: moveX={animator.GetFloat("moveX")}, moveY={animator.GetFloat("moveY")}");
    }

    // Metode ini dipanggil oleh event animasi pada akhir animasi memukul
    public void EndAttack()
    {
        isAttacking = false;
        animator.SetBool("isAttacking", false);
        Debug.Log("Attack ended");
    }
}
