using UnityEngine;

public class AttackHitbox : MonoBehaviour
{
    public int attackDamage = 40; // Jumlah damage yang diberikan
    private Vector2 offset;

    void Update()
    {
        // Update the position of the collider based on the offset
        transform.localPosition = offset;
    }

    public void SetOffset(Vector2 direction)
    {
        float distance = 0.5f; // Adjust this value based on how far you want the hitbox to be from the character
        offset = direction.normalized * distance;
    }

    public void FlipHitbox(Vector2 direction)
    {
        // Flip the hitbox based on the direction
        Vector3 scale = transform.localScale;

        if (direction.x != 0)
        {
            scale.x = Mathf.Sign(direction.x);
        }

        if (direction.y != 0)
        {
            scale.y = Mathf.Sign(direction.y);
        }

        transform.localScale = scale;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("enemy"))
        {
            Debug.Log("Dapat Musuh");
            other.GetComponent<Enemy>().TakeDamage(attackDamage);
        }
    }

    
}
