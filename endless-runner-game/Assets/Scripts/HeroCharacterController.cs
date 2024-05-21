using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroCharacterController : MonoBehaviour
{
    [SerializeField] LayerMask groundLayers;
    [SerializeField] private float runSpeed = 8f;
    [SerializeField] private float jumpHeight = 2f;
    [SerializeField] private float acceleration = 0.1f; // Speed increase per second
    [SerializeField] private float dropSpeed = -20f; // Initial drop speed
    [SerializeField] private float dropAcceleration = -5f; // Drop speed increase per second

    private float gravity = -50f;
    private CharacterController characterController;
    private Animator animator;
    private Vector3 velocity;
    private bool isGrounded;
    private float initialRunSpeed;
    private float horizontalInput;
    private float initialDropSpeed;

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        initialDropSpeed = dropSpeed;
        initialRunSpeed = runSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        // Always one since it's an automatic endless runner
        horizontalInput = 1;

        // Increase run speed over time
        runSpeed = initialRunSpeed + (acceleration * Time.time);

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
            velocity.y += Mathf.Sqrt(jumpHeight * -2 * gravity);
        }

        // Update drop speed over time
        dropSpeed = initialDropSpeed + (dropAcceleration * Time.deltaTime);

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
}
