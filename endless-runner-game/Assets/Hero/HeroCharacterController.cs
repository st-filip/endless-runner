using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroCharacterController : MonoBehaviour
{
    [SerializeField] LayerMask groundLayers;
    [SerializeField] private float runSpeed = 8f;
    [SerializeField] private float jumpHeight = 2;

    private float gravity = -50f;
    private CharacterController characterController;
    private Animator animator;
    private Vector3 velocity;
    private bool isGrounded;
    private float horizontalInput;
    

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();   
        animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        // Always one since it's an automatic endless runner
        horizontalInput = 1;
        // Face forward
        transform.forward = new Vector3 (horizontalInput, 0, Mathf.Abs(horizontalInput) - 1);

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

        // Move vertically (fall)
        characterController.Move(velocity * Time.deltaTime);

        // Jump
        if (isGrounded && Input.GetButtonDown("Jump")) 
        {
            velocity.y += Mathf.Sqrt(jumpHeight * -2 * gravity);
        }

        // Set animator speed for run animation
        animator.SetFloat("Speed", horizontalInput);

        // Set animator IsGrounded
        animator.SetBool("IsGrounded", isGrounded);

        // Set animator VerticalSpeed for jump/fall animation
        animator.SetFloat("VerticalSpeed", velocity.y);
    }
}
