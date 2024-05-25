using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterButton : MonoBehaviour
{
    [SerializeField] private int characterIndex; // Assign a unique index for each character button
    [SerializeField] private Material selectedMaterial;
    [SerializeField] private Material unselectedMaterial;

    private TMPro.TextMeshProUGUI buttonText;

    // Start is called before the first frame update
    void Start()
    {
        Button button = GetComponent<Button>();
        button.onClick.AddListener(OnCharacterSelected);
        buttonText = GetComponentInChildren<TMPro.TextMeshProUGUI>();
        UpdateButtonMaterial();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCharacterSelected()
    {
        CharacterSelection.Instance.SelectCharacter(characterIndex);
        Debug.Log("Character " + characterIndex + " selected.");

    }

    public void UpdateButtonMaterial()
    {
        if (CharacterSelection.Instance.GetSelectedCharacterIndex() == characterIndex)
        {
            buttonText.fontMaterial = selectedMaterial;
        }
        else
        {
            buttonText.fontMaterial = unselectedMaterial;
        }
    }
}
