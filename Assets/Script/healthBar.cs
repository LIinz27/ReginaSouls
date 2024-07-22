using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class healthBar : MonoBehaviour
{
    public Image image;
    public float health = 100f;
    public GameObject[] gameOver;

    public bool isGameOver = false;
    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateText(float damage)
    {
        
        health -= damage;
        if (health > 100)
        {
            health = 100;
        }
        image.fillAmount = health / 100f;
        if(health <= 0)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            Destroy(player);

            foreach (GameObject gob in GameObject.FindGameObjectsWithTag("potion"))
            {
                Destroy(gob);
            }

            foreach (GameObject gob in gameOver) {
                
                gob.SetActive(true);
                GameManager.killEnemy = 0;
            } 
        }
    }
}
