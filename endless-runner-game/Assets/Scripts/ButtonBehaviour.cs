using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonBehaviour : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Texture2D defaultCursor;
    [SerializeField] private Texture2D clickerCursor;
    private Vector2 cursorHotspot = Vector2.zero; // Adjust as needed

    private void Start()
    {
        Button button = GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(() => AudioManager.Instance.PlaySound("Button"));
        }

        // Set the default cursor initially
        Cursor.SetCursor(defaultCursor, cursorHotspot, CursorMode.Auto);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // Change the cursor to the clicker cursor when hovering over the button
        Cursor.SetCursor(clickerCursor, cursorHotspot, CursorMode.Auto);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // Revert the cursor back to the default cursor when not hovering over the button
        Cursor.SetCursor(defaultCursor, cursorHotspot, CursorMode.Auto);
    }
}
