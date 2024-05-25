using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject gameOverlayPanel;
    [SerializeField] private TMPro.TextMeshProUGUI scoreText;
    [SerializeField] private TMPro.TextMeshProUGUI scoreIncrement;
    [SerializeField] private List<RawImage> hearts;
    [SerializeField] private Image hurt;

    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private TMPro.TextMeshProUGUI finalScoreText;

    [SerializeField] private GameObject[] characters;

    [SerializeField] private GameObject pausePanel;

    private int score;
    public static GameManager instance;
    private int heartCount = 3;
    private bool isPaused = false;

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
        Time.timeScale = 1f;

        int selectedCharacterIndex = CharacterSelection.Instance ? CharacterSelection.Instance.GetSelectedCharacterIndex() - 1 : 0;

        for (int i = 0; i < characters.Length; i++)
        {
            if(i != selectedCharacterIndex)
            {
                characters[i].gameObject.SetActive(false);
            }
            else
            {
                characters[i].gameObject.SetActive(true);
            }
            
        }

        instance.scoreText.text = 0 + "";
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        isPaused = true;
        pausePanel.SetActive(true);
        Time.timeScale = 0f; // Pause the game
    }

    public void ResumeGame()
    {
        isPaused = false;
        pausePanel.SetActive(false);
        Time.timeScale = 1f; // Resume the game
    }

    public void IncrementScore(int inc)
    {
        instance.score += inc;
        instance.scoreText.text = instance.score.ToString();
        instance.scoreIncrement.text = "+" + inc.ToString();
        StartCoroutine(FadeOutIncrement(inc));
    }

    private IEnumerator FadeOutIncrement(int inc)
    {
        // Adjust color based on the score increment value
        Color incrementColor = (inc == 5) ? Color.green : Color.white;
        incrementColor.a = 1f; // Set alpha to 100%

        scoreIncrement.color = incrementColor; // Assign the modified color back to the score increment text

        // Adjust font size based on the score increment value
        int fontSize = (inc == 5) ? 150 : 100;
        scoreIncrement.fontSize = fontSize;

        float fadeDuration = 0.7f; // Duration of the fade-out animation

        // Gradually fade out
        float elapsed = 0f;
        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / fadeDuration);

            incrementColor.a = Mathf.Lerp(1f, 0f, t); // Decrease alpha gradually
            scoreIncrement.color = incrementColor; // Update the color of the score increment text

            yield return null; // Wait for the next frame
        }
    }


    private IEnumerator FadeOutHurt()
    {
        Color hurtColor = hurt.color; // Get the current color of the "hurt" Image
        hurtColor.a = 1f; // Set alpha to 100%
        hurt.color = hurtColor; // Assign the modified color back to the "hurt" Image

        float fadeDuration = 0.7f; // Duration of the fade-out animation

        // Gradually fade out
        float elapsed = 0f;
        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / fadeDuration);

            hurtColor.a = Mathf.Lerp(1f, 0f, t); // Decrease alpha gradually
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

    public void GameOver()
    {
        instance.finalScoreText.text = "Score: " + instance.score;
        // Deactivate the GameOverlay panel
        instance.gameOverlayPanel.SetActive(false);
        // Activate the GameOver panel
        instance.gameOverPanel.SetActive(true);
        Debug.Log("Game over");
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadSceneAsync(0);
    }
    public void PlayAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
