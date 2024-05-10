using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class InfiniteCarController : MonoBehaviour
{
    public int remainingJumps = 0;
    public bool canJump = false;
    public float moveSpeed = 10f;
    public float turnSpeed = 2f;
    public float jumpForce = 10f;
    public float gravity = 20f;
    public float jumpHeight = 2f;
    public float jumpCooldown = 1f;
    public Rigidbody rb;
    public bool isGrounded = true;
    [SerializeField] private TextMeshProUGUI jumpCountText;
    [SerializeField] private PlayerInput playerInput;
    private bool canControl = true; // Flag to indicate whether the player can control the car

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerInput.actions["Jump"].performed += OnJumpPerformed;
        remainingJumps = 0;
        UpdateJumpCountText();
    }

    private void OnJumpPerformed(InputAction.CallbackContext context)
    {
        Jump();
    }

    private void Update()
    {
        if (canControl)
        {
            // Handle player control inputs only if canControl is true
            // Implement your player control logic here
            // Check if the player has collected the jump pickup and is grounded
            if (isGrounded && remainingJumps > 0)
            {
                canJump = true; // Allow the player to jump
            }
            else
            {
                canJump = false; // Prevent the player from jumping
            }

            // Rotate the car based on the horizontal input
            float horizontalInput = playerInput.actions["MoveRight"].ReadValue<float>() - playerInput.actions["MoveLeft"].ReadValue<float>();
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
            transform.Rotate(Vector3.up, horizontalInput * turnSpeed * Time.deltaTime);
        }

        if (transform.position.y < -10)
        {
            // If the car falls below the level, end the game
            FindObjectOfType<GameManager>().RestartGame();
        }


    }

    private void FixedUpdate()
    {
        if (!isGrounded)
        {
            rb.AddForce(Vector3.down * gravity, ForceMode.Acceleration);
        }
    }

    public void Jump()
    {
        if (canJump)
        {
            Debug.Log("Jumping! Remaining Jumps: " + remainingJumps); // Add this line
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            remainingJumps--;
            UpdateJumpCountText();
            isGrounded = false;
            canJump = false;
            StartCoroutine(JumpCooldown());
        }
    }

    private IEnumerator JumpCooldown()
    {
        Debug.Log("Jump Cooldown"); // Add this line
        yield return new WaitForSeconds(jumpCooldown);
        UpdateJumpCountText();
        canJump = true; // Reset canJump flag
        isGrounded = true; // Reset isGrounded flag
        Debug.Log("Jump Cooldown Over. Remaining Jumps: " + remainingJumps); // Add this line
    }

    private void UpdateJumpCountText()
    {
        jumpCountText.text = remainingJumps.ToString();
    }


    public void SetCanControl(bool value)
    {
        canControl = value;
    }
}
