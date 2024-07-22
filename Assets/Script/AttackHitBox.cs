using UnityEngine;

public class AttackHitbox : MonoBehaviour
{
    public int attackDamage = 40; // Jumlah damage yang diberikan

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("enemy"))
        {
            Debug.Log("Dapat Musuh");
            other.GetComponent<Enemy>().TakeDamage(attackDamage);
         
        }
    }
}


