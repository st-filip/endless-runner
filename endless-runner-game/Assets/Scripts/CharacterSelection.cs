using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterSelection : MonoBehaviour
{
    public static CharacterSelection Instance;

    private int selectedCharacterIndex = 1; // Index of the selected character

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
    }

    public void SelectCharacter(int index)
    {
        selectedCharacterIndex = index;
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
