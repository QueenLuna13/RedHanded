using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    private float normalSpeed = 5;
    private float sprintSpeed = 15;
    private float stamina = 3f;
    private float maxStamina = 3f;
    private float staminaDepletionRate = 1f;
    public Image staminaBar;

    private float horizontalInput;
    private float forwardInput;

    private Vector3 originalPlayerScale;
    private bool isCrouching = false;

    public CameraController cameraController;
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        originalPlayerScale = transform.localScale;

        if (cameraController == null)
        {
            cameraController = Camera.main.GetComponentInParent<CameraController>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        forwardInput = Input.GetAxis("Vertical");

        // Check if sprint button is pressed and there's enough stamina
        bool isSprinting = Input.GetKey(KeyCode.LeftShift) || Input.GetButton("Fire3") && stamina > 0;

        // Adjust speed based on sprinting and manage stamina
        float currentSpeed = isSprinting ? sprintSpeed : normalSpeed;
        if (isSprinting)
        {
            stamina -= Time.deltaTime * staminaDepletionRate; // Adjust the depletion rate as needed
            stamina = Mathf.Clamp(stamina, 0f, maxStamina);

            // If stamina is depleted, prevent further depletion
            if (stamina == 0f)
            {
                currentSpeed = normalSpeed;
            }
        }
        else
        {
            // Regenerate stamina if not sprinting and not holding the sprint button
            if (!Input.GetKey(KeyCode.LeftShift) || Input.GetButton("Fire3") && stamina < maxStamina)
            {
                stamina += Time.deltaTime * staminaDepletionRate;
                stamina = Mathf.Clamp(stamina, 0f, maxStamina);
            }
        }

        Vector3 forward = cameraController.transform.forward;
        forward.y = 0f;
        forward.Normalize();
        Vector3 right = cameraController.transform.right;
        right.y = 0f;
        right.Normalize();

        // Calculate the movement direction based on camera rotation
        Vector3 moveDirection = forward * forwardInput + right * horizontalInput;

        // Apply force to the Rigidbody for smoother movement
        rb.velocity = new Vector3(moveDirection.x * currentSpeed, rb.velocity.y, moveDirection.z * currentSpeed);

        // Check if crouch button is pressed
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("Jump"))
        {
            Crouch();
        }
        // Check if crouch button isn't pressed
        if (Input.GetKeyUp(KeyCode.Space) || Input.GetButtonUp("Jump"))
        {
            Uncrouch();
        }

        UpdateStaminaUI();
    }

    void Crouch()
    {
        if (!isCrouching)
        {
            isCrouching = true;
            transform.localScale = originalPlayerScale * 0.5f;
            transform.position = new Vector3(transform.position.x, transform.position.y - 0.5f, transform.position.z);
        }
    }

    void Uncrouch()
    {
        if (isCrouching)
        {
            isCrouching = false;
            transform.localScale = originalPlayerScale;
            transform.position = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);
        }
    }

    void UpdateStaminaUI()
    {
        if (staminaBar != null)
        {
            staminaBar.fillAmount = stamina / maxStamina;
        }
    }
}
