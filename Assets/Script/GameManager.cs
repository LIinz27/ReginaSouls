using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour {
    
    public static GameManager instance;

    public Character[] characters;

    public Character currentCharacter;

    public static int killEnemy = 0;

    

    public static void UpdateText()
    {
        TMP_Text score = GameObject.FindGameObjectWithTag("score")?.GetComponent<TMP_Text>();
        
        score.SetText("Kill : "+killEnemy + " / "+ FindFirstObjectByType<gamestate>().jumlahEnemy);
    }

   

    private void Awake() {
        if (instance == null) {
            instance = this;
        } else {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    private void Start() {
        if (characters.Length > 0) {
            currentCharacter = characters[0];
        }
    }

    public void SetCharacter(Character character) {
        currentCharacter = character;
    }
}
