using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnEnemy : MonoBehaviour
{

    public GameObject[] enemy;
    public int jumlahEnemy;
    public int interval;
    public GameObject[] spawnPoints;
    private int curenemy = 0;
    
    

    // Update is called once per frame
    void Update()
    {
        
    }

    void Start()
    {
        GameManager.UpdateText();
        
    }

    public void startGenerate()
    {
        InvokeRepeating("RepeatingMethod", 0f, interval);
    }

    public void spawnBos()
    {
        int indLast = Random.Range(0, spawnPoints.Length);


        GameObject curInds = spawnPoints[indLast];
        Instantiate(enemy[enemy.Length - 1], curInds.transform.position, Quaternion.identity);
        return;
    }

    void RepeatingMethod()
    {
        
        
        if (curenemy >= jumlahEnemy )
        {
            CancelInvoke("RepeatingMethod");
            return;
        }
        curenemy++;
        int ind = Random.Range(0, spawnPoints.Length);
        int ind2 = Random.Range(0, enemy.Length-1);

        GameObject gm = enemy[ind2];

        
        GameObject curInd = spawnPoints[ind];
        Instantiate(gm, curInd.transform.position, Quaternion.identity);

    }

    void OnDisable()
    {
        // Cancel the scheduled method calls when the script is disabled or destroyed
        CancelInvoke("RepeatingMethod");
    }
}
