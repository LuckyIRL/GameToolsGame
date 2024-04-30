using UnityEngine;
using UnityEngine.UI;
using System.Collections;
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
    [SerializeField] private GameObject jumpIcon;
    [SerializeField] public PlayerInput playerInput;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerInput.actions["Jump"].performed += OnJumpPerformed;
        playerInput.actions["MoveLeft"].ReadValue<float>();
        playerInput.actions["MoveRight"].ReadValue<float>();
        remainingJumps  = 0;
        jumpIcon.SetActive(false);
    }

    private void OnJumpPerformed(InputAction.CallbackContext context)
    {
        Jump();
    }

    private void Update()
    {
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


    private void FixedUpdate()
    {
        if (!isGrounded)
        {
            rb.AddForce(Vector3.down * gravity, ForceMode.Acceleration);
        }
    }





    public void Jump()
    {
        if (canJump && remainingJumps > 0)
        {
            Debug.Log("Jump called.");
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            Debug.Log("Jumped! Remaining Jumps: " + remainingJumps);
            remainingJumps--;
            jumpIcon.SetActive(false);
            StartCoroutine(JumpCooldown());
        }
    }




    private IEnumerator JumpCooldown()
    {
        yield return new WaitForSeconds(jumpCooldown);
        remainingJumps++;
    }

}
