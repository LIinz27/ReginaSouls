using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public AttackHitbox attackHitbox;
    public float moveSpeed = 5f;
    public Transform attackPoint; // Referensi ke GameObject AttackHitbox
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
        attackPoint.gameObject.SetActive(false); // Pastikan hitbox tidak aktif di awal
    }

    private void Update()
    {
        rb.velocity = Vector3.zero;

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

        // Set the hitbox offset based on the last move direction
        Vector2 attackDirection = new Vector2(animator.GetFloat("moveX"), animator.GetFloat("moveY"));
        attackHitbox.SetOffset(attackDirection);
        attackHitbox.FlipHitbox(attackDirection);
        
        Debug.Log($"Attack started: moveX={animator.GetFloat("moveX")}, moveY={animator.GetFloat("moveY")}");
    }

    // Metode ini dipanggil oleh event animasi pada akhir animasi memukul
    public void EndAttack()
    {
        isAttacking = false;
        animator.SetBool("isAttacking", false);
        Debug.Log("Attack ended");
    }

    // Metode ini dipanggil oleh event animasi pada awal animasi memukul
    public void EnableHitbox()
    {
        attackPoint.gameObject.SetActive(true);
    }

    // Metode ini dipanggil oleh event animasi pada akhir animasi memukul
    public void DisableHitbox()
    {
        attackPoint.gameObject.SetActive(false);
    }
}
