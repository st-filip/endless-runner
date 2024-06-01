using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSound : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Button button = GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(() => AudioManager.Instance.PlaySound("Button"));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
