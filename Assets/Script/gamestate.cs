using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class gamestate : MonoBehaviour
{
    public int Level = 1;
    public spawnEnemy[] spawns;
    public PlayerSpawner[] playerSpawn;
    public potionSpawn[] potionspawns;
    public GameObject winText;
    public GameObject restartText;
    public int jumlahEnemy;
    public int curPotion = 0;
    public int maxPotion;
    public TMP_Text curPotionText;

    // Start is called before the first frame update
    void Start()
    {
        Level = 1;
        spawns[0].startGenerate();
        playerSpawn[0].spawnPlayerNow();
        potionspawns[0].startSpawn();
        jumlahEnemy = spawns[0].jumlahEnemy;
        GameManager.killEnemy = 0;

        curPotion = 3;
        UpdateTextPotion();
    }



    public void addLevel()
    {
        potionspawns[Level - 1].CancelSpawn();
        foreach(GameObject obat in GameObject.FindGameObjectsWithTag("potion"))
        {
            Destroy(obat);
        }
        
        if(Level  >= spawns.Length)
        {
            winText.SetActive(true);
            restartText.SetActive(true);
            Destroy(GameObject.FindGameObjectWithTag("Player"));
            GameManager.killEnemy = 0;
            return;
        }
        Level++;
        jumlahEnemy = spawns[Level - 1].jumlahEnemy;
        spawns[Level - 1].startGenerate();
        playerSpawn[Level - 1].spawnPlayerNow();
        potionspawns[Level - 1].startSpawn();
        GameManager.killEnemy = 0;
        UpdateTextPotion();
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && curPotion > 0)
        {
            healthBar health = GameObject.FindFirstObjectByType<healthBar>();
            if (health.health < 100)
            {
                curPotion--;

                health.UpdateText(-50);
                UpdateTextPotion();
            }
        }
    }



    public void tambahPotion(int jumlah)
    {
        curPotion += jumlah;
        if (curPotion > maxPotion)
        {
            curPotion = maxPotion;
        }
    }

    public void UpdateTextPotion()
    {
        curPotionText.text = ": " + curPotion;
    }
}
