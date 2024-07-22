using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class potionSpawn : MonoBehaviour
{

    

    public GameObject potion;

    public int interval = 60;

    
    // Start is called before the first frame update
    void Start()
    {

        
        
    }

    public void startSpawn()
    {
        InvokeRepeating("RepeatingMethod", 0f, interval);
    }

    private void Update()
    {
        
    }

    

   

    void RepeatingMethod()
    {

        int ind = Random.Range(0, transform.childCount);
       
        Instantiate(potion,  transform.GetChild(ind).position,Quaternion.identity);

    }

    public void CancelSpawn()
    {
        CancelInvoke("RepeatingMethod");
    }

    void OnDisable()
    {
        // Cancel the scheduled method calls when the script is disabled or destroyed
        CancelInvoke("RepeatingMethod");
    }
}
