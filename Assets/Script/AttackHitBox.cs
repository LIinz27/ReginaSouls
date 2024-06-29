using UnityEngine;

public class AttackHitbox : MonoBehaviour
{
    public int attackDamage = 40; // Jumlah damage yang diberikan

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<Enemy>().TakeDamage(attackDamage);
        }
    }
}
