using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    [SerializeField] private TMPro.TextMeshProUGUI scoreText;
    [SerializeField] private List<RawImage> hearts;
    [SerializeField] private Image hurt;

    private int score;
    public static GameManager instance;
    private int heartCount = 3;

    // Singleton
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        instance.scoreText.text = 0 + "";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void IncrementScore()
    {
        instance.score++;
        instance.scoreText.text = instance.score.ToString();
    }
    private IEnumerator FadeOutHurt()
    {
        Color hurtColor = hurt.color; // Get the current color of the "hurt" Image
        hurtColor.a = 1f; // Set alpha to 100%
        hurt.color = hurtColor; // Assign the modified color back to the "hurt" Image

        // Wait for a short duration
        yield return new WaitForSeconds(1f);

        // Gradually fade out
        while (hurtColor.a > 0f)
        {
            hurtColor.a -= Time.deltaTime * 2f; // Decrease alpha gradually
            hurt.color = hurtColor; // Update the color of the "hurt" Image
            yield return null; // Wait for the next frame
        }
    }

    public void RemoveHeart()
    {
        if (heartCount > 0)
        {
            heartCount--;
            Color heartColor = hearts[heartCount].color;
            heartColor.a = 0.7f; // Set the alpha to 0.7
            hearts[heartCount].color = heartColor;
            StartCoroutine(FadeOutHurt());
        }
    }
}
