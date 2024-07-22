using UnityEngine;

public class PlayerSpawner : MonoBehaviour {
    
    private void Start() {
    }

    public void spawnPlayerNow()
    {
        Instantiate(GameManager.instance.currentCharacter.prefab, transform.position, Quaternion.identity);

    }

}
