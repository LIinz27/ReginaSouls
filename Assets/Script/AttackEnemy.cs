using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackEnemy : MonoBehaviour
{

    public int attackDamage = 30; // Jumlah damage yang diberikan

    private void OnTriggerEnter2D(Collider2D other)
    {
        
        
        if (other.CompareTag("Player"))
        {
            Enemy musuh = gameObject.GetComponentInParent<Enemy>();
            healthBar health = GameObject.FindFirstObjectByType<healthBar>();
            
            health.UpdateText(20);
           
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
