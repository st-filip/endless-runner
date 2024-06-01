using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterSelection : MonoBehaviour
{
    public static CharacterSelection Instance;

    private int selectedCharacterIndex; // Index of the selected character

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Make this object persistent across scenes
        }
        else
        {
            Destroy(gameObject);
        }

        // Load the saved character index or set to default value if not found
        selectedCharacterIndex = PlayerPrefs.GetInt("SelectedCharacterIndex", 1);
    }

    public void SelectCharacter(int index)
    {
        selectedCharacterIndex = index;
        PlayerPrefs.SetInt("SelectedCharacterIndex", selectedCharacterIndex); // Save to PlayerPrefs
        PlayerPrefs.Save(); // Ensure the changes are saved
        UpdateCharacterButtons();
    }

    public int GetSelectedCharacterIndex()
    {
        return selectedCharacterIndex;
    }

    public void UpdateCharacterButtons()
    {
        CharacterButton[] characterButtons = FindObjectsOfType<CharacterButton>();
        foreach (CharacterButton button in characterButtons)
        {
            button.UpdateButtonMaterial();
        }
    }
}
