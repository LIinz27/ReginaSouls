using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Timeline.TimelinePlaybackControls;

public class Enemy : MonoBehaviour
{
    public float health = 100;
    public float moveSpeed = 3f;
    public Vector2 lastMoveDirection;
    private Rigidbody2D rb;
    private Animator animator;
    private bool stopping = false;
    public bool isAttack = false;
    public Transform attackPoint;
    public Canvas canvas;
    public Image healthImage;
    private bool canvasActive = false;
    public bool isArcher = false;
    public bool isBos = false;
    public bool isFlip;
    private float maxHealth;

    public GameObject mushroom;
    public float cooldown = 5f;
    public float cooldownAttack = 2f;
    float timestamp = 0f;
    float timestampAttack = 0f;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        maxHealth = health;
    }

    private void Update()
    {
        if (!stopping)
        {
            MoveTowardsPlayer();
        }
    }

    private void MoveTowardsPlayer()
    {
        Transform player = GameObject.FindGameObjectWithTag("Player")?.transform;
        rb.velocity = Vector3.zero;
        if(player != null && (isArcher || isBos) && Vector2.Distance(transform.position, player.position) < 5)
        {
            Animator parentAnimator = gameObject.GetComponentInParent<Animator>();
            if(!isBos)
            {
                parentAnimator.SetBool("isWalking", false);
            }
            if (timestamp < Time.time)
            {
                isAttack = true;
                mushroomArrow arr = Instantiate(mushroom, transform).GetComponent<mushroomArrow>();
                arr.initPosition(new Vector2(player.transform.position.x, player.transform.position.y));

                parentAnimator.SetBool("isAttacking", true);
                timestamp = Time.time + cooldown;
            }
            return;
        }
        if (player != null && !isAttack )
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
        }

        if (transform.position != Vector3.zero && !isAttack)
        {
          
           // Update arah terakhir saat bergerak
              SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
            if(transform.position.x > lastMoveDirection.x)
            {
                spriteRenderer.flipX = isFlip;
            } else
            {
                spriteRenderer.flipX = !isFlip;
            }

            lastMoveDirection = transform.position;
            animator.SetBool("isWalking", true);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Player" && !isArcher)
        {
            if(isAttack == false && !stopping && timestampAttack < Time.time)
            {
                isAttack = true;
                animator.SetBool("isWalking", false);
                animator.SetBool("isAttacking", true);
                timestampAttack = Time.time;
            }

        }
    }

    public void EnableHitbox()
    {
        if(!stopping)
        {
            attackPoint.gameObject.SetActive(true);
        }

    }

    // Metode ini dipanggil oleh event animasi pada akhir animasi memukul
    public void EndHitbox()
    {
        attackPoint.gameObject.SetActive(false);
      
        

    }

    public void EndAttack()
    {
        isAttack = false;
        animator.SetBool("isAttacking", false);
    }

    public void TakeDamage(int damage)
    {
        if(canvasActive == false)
        {
            canvas.enabled = true;
            canvasActive = true;
        }
        health -= damage;
        
        healthImage.fillAmount = health / maxHealth;
        if (health <= 0)
        {
            isAttack = false;
            animator.SetBool("isAttacking", false);
            animator.SetBool("isWalking", false);
            animator.SetBool("isDead", true);
            stopping = true;
            
        }
    }

    private void Die()
    {
        GameManager.killEnemy++;
        GameManager.UpdateText();
       
        gamestate state = FindFirstObjectByType<gamestate>();
        if (GameManager.killEnemy == state.jumlahEnemy)
        {
            state.spawns[state.Level - 1].spawnBos();
        } else if (GameManager.killEnemy >= state.jumlahEnemy + 1)
        {
            
            Destroy(GameObject.FindGameObjectWithTag("Player"));
            state.addLevel(); 
        }

        Destroy(gameObject);
    }
}
