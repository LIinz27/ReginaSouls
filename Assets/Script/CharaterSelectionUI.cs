using UnityEngine;
using UnityEngine.UI;

public class CharacterSelectionUI : MonoBehaviour {
    public GameObject optionPrefab;

    public Transform prevCharacter;
    public Transform selectedCharacter;

    private void Start() {
        if (GameManager.instance == null) {
            Debug.LogError("GameManager instance is null");
            return;
        }

        if (GameManager.instance.characters == null) {
            Debug.LogError("GameManager characters are null");
            return;
        }

        foreach (Character c in GameManager.instance.characters) {
            if (optionPrefab == null) {
                Debug.LogError("OptionPrefab is not assigned");
                return;
            }

            GameObject option = Instantiate(optionPrefab, transform);
            Button button = option.GetComponent<Button>();

            if (button == null) {
                Debug.LogError("Button component is missing on optionPrefab");
                return;
            }

            button.onClick.AddListener(() => {
                GameManager.instance.SetCharacter(c);
                if (selectedCharacter != null) {
                    prevCharacter = selectedCharacter;
                }

                selectedCharacter = option.transform;
            });

            Text text = option.GetComponentInChildren<Text>();
            if (text != null) {
                text.text = c.name;
            } else {
                Debug.LogError("Text component is missing on optionPrefab");
            }

            Image image = option.GetComponentInChildren<Image>();
            if (image != null) {
                image.sprite = c.icon;
            } else {
                Debug.LogError("Image component is missing on optionPrefab");
            }
        }
    }

    private void Update() {
        if (selectedCharacter != null) {
            selectedCharacter.localScale = Vector3.Lerp(selectedCharacter.localScale, new Vector3(1.2f, 1.2f, 1.2f), Time.deltaTime * 10);
        }

        if (prevCharacter != null) {
            prevCharacter.localScale = Vector3.Lerp(prevCharacter.localScale, new Vector3(1f, 1f, 1f), Time.deltaTime * 10);
        }
    }
}
