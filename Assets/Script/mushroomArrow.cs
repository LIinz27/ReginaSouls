using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mushroomArrow : MonoBehaviour
{
    public Vector2 positionCur;
    public Vector2 pos;
    Animator animator;
    private bool stop = false;
    public int attackDamage = 10; // Jumlah damage yang diberikan

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void initPosition(Vector2 pos)
    {
        positionCur = pos;
    }

    // Update is called once per frame
    void Update()
    {
        if(!stop)
        {
            transform.position = Vector2.MoveTowards(transform.position, positionCur, 2f * Time.deltaTime) ;
            if (Vector2.Distance(transform.position, positionCur) < 0.5)
            {
                stop = true;
                animator.SetBool("destroy", true);
            }

            if (transform.position != Vector3.zero)
            {

                // Update arah terakhir saat bergerak
                SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
                if (transform.position.x > pos.x)
                {
                    spriteRenderer.flipX = false;
                }
                else
                {
                    spriteRenderer.flipX = true;
                }

                pos = transform.position;
                
            }
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Enemy musuh = gameObject.GetComponentInParent<Enemy>();
            healthBar health = GameObject.FindFirstObjectByType<healthBar>();

            health.UpdateText(20);

            animator.SetBool("destroy", true);
            stop = true;
        }
    }

    void destroySelf()
    {
        Destroy(gameObject);
    }
}
