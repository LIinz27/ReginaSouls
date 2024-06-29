using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health = 100;
    public float moveSpeed = 3f;
    private Transform player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        MoveTowardsPlayer();
    }

    private void MoveTowardsPlayer()
    {
        if (player != null)
        {
            Vector2 direction = (player.position - transform.position).normalized;
            transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log("Enemy took damage: " + damage);

        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        // Tambahkan logika untuk menghancurkan atau menonaktifkan musuh
        Debug.Log("Enemy died");
        Destroy(gameObject);
    }
}
