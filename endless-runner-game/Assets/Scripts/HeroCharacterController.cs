using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HeroCharacterController : MonoBehaviour
{
    [SerializeField] LayerMask groundLayers;
    [SerializeField] private float runSpeed = 8f;
    [SerializeField] private float jumpHeight = 2f;
    [SerializeField] private float acceleration = 0.1f; // Speed increase per second
    [SerializeField] private float dropSpeed = -20f; // Initial drop speed
    [SerializeField] private float dropAcceleration = -5f; // Drop speed increase per second
    [SerializeField] private int countdownTime = 3;
    [SerializeField] private Material countMaterial;
    [SerializeField] private Material goMaterial;

    private float gravity = -50f;
    private CharacterController characterController;
    private Animator animator;
    private Vector3 velocity;
    private bool isGrounded;
    private float initialRunSpeed;
    private float horizontalInput;
    private float initialDropSpeed;
    private int sweetCount = 0;
    private float elapsedTime = 0f;
    private bool canRun = false;

    Dictionary<string, string> favoritePairs = new Dictionary<string, string>
    {
        { "FApple(Clone)", "Tica" },
        { "FPear(Clone)", "Ogi" },
        { "FBanana(Clone)", "Fica" },
        { "FStrawberry(Clone)", "Ema" }
    };

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        animator.SetFloat("Speed", 0f);
        animator.SetBool("IsGrounded", true);
        initialDropSpeed = dropSpeed;
        initialRunSpeed = runSpeed;
        StartCoroutine(StartCountdown());
    }

    private IEnumerator StartCountdown()
    {
        GameObject countdownOverlay = GameObject.Find("CountdownOverlay");
        countdownOverlay.SetActive(true);
        TextMeshProUGUI countdownText = countdownOverlay.transform.Find("CountdownText").GetComponent<TextMeshProUGUI>();

        int countdown = countdownTime+1;
        while (countdown > 0)
        {
            if (countdown != 1)
            {
                countdownText.text = (countdown - 1).ToString();
                countdownText.fontMaterial = countMaterial; // Set count material
                countdownText.fontSize = 250;
            }
            else
            {
                countdownText.text = "GO!";
                countdownText.fontMaterial = goMaterial; // Change to "GO!" material
                countdownText.fontSize = 300;
            }

            yield return new WaitForSeconds(1);
            countdown--;
        }

        countdownOverlay.SetActive(false);
        HeroCharacterController hero = FindObjectOfType<HeroCharacterController>();
        if (hero != null)
        {
            hero.StartRunning();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale == 0f) return;

        if (!canRun) return;

        if (sweetCount >= 3 || transform.position.y <= -10)
        {
            // Stop movement and disable this script
            enabled = false;
            runSpeed = 0f;
            animator.SetFloat("Speed", 0f);
            GameManager.instance.GameOver();
            AudioManager.Instance.PlaySound("GameOver");
            return;
        }

        // Increment elapsed time
        elapsedTime += Time.deltaTime;

        // Always one since it's an automatic endless runner
        horizontalInput = 1;

        // Increase run speed over time
        runSpeed = initialRunSpeed + (acceleration * elapsedTime);

        // Face forward
        transform.forward = new Vector3(horizontalInput, 0, Mathf.Abs(horizontalInput) - 1);

        isGrounded = Physics.CheckSphere(transform.position, 0.1f, groundLayers, QueryTriggerInteraction.Ignore);

        // Apply gravity only if grounded
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = 0;
        }
        else
        {
            velocity.y += gravity * Time.deltaTime;
        }

        // Move horizontally (run)
        characterController.Move(new Vector3(horizontalInput * runSpeed, 0, 0) * Time.deltaTime);

        // Check for jump input
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            AudioManager.Instance.PlaySound("Jump");
            velocity.y += Mathf.Sqrt(jumpHeight * -2 * gravity);
        }

        // Update drop speed over time
        dropSpeed = initialDropSpeed + (dropAcceleration * elapsedTime);

        // Check for drop input (Enter key)
        if (!isGrounded && Input.GetKeyDown(KeyCode.Return))
        {
            velocity.y = dropSpeed;
        }

        // Move vertically (fall or jump or drop)
        characterController.Move(velocity * Time.deltaTime);

        // Set animator speed for run animation
        animator.SetFloat("Speed", horizontalInput);

        // Set animator IsGrounded
        animator.SetBool("IsGrounded", isGrounded);

        // Set animator VerticalSpeed for jump/fall animation
        animator.SetFloat("VerticalSpeed", velocity.y);
    }
    public void StartRunning()
    {
        canRun = true;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Sweet"))
        {
            sweetCount++;
            GameManager.instance.RemoveHeart();
        }

        if (other.CompareTag("Fruit"))
        {
            bool isFavorite = false;

            // Check if the colliding object's name is in the dictionary
            if (favoritePairs.ContainsKey(other.gameObject.name))
            {
                string characterName = favoritePairs[other.gameObject.name];
                GameObject characterObject = GameObject.Find(characterName);

                if (characterObject != null && characterObject.activeInHierarchy)
                {
                    isFavorite = true;
                }
            }

            int inc = (isFavorite) ? 5 : 1;
            GameManager.instance.IncrementScore(inc);

        }
    }
}
