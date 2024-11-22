using UnityEngine;

public class ThridPersonMovement : MonoBehaviour
{
    CharacterController controller;
    [SerializeField] Transform cam;

    [HideInInspector] public bool canMove = true;
    [SerializeField] float speed;
    [SerializeField] float hoverHeight;
    [Range(0, 1)][SerializeField] float turnSmoothTime;

    float turnSmoothVelocity = 1f;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        cam = Camera.main?.transform;
    }

    void Update()
    {
        // Get player input
        var horizontal = Input.GetAxisRaw("Horizontal");
        var vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0, vertical).normalized;

        // Move player if there's input
        if (direction.magnitude >= 0.1f)
        {
            movePlayer(direction);
        }

        // Handle hover height adjustment
        HandleHoverHeight();
    }

    void HandleHoverHeight()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit))
        {

            // Log detected height for debugging
            Debug.Log($"Ground Detected at: {hit.point.y}");

            // Calculate target hover height
            float hitHeight = hit.point.y;
            float targetHeight = hitHeight + hoverHeight;

            // Smoothly interpolate toward the target height
            float smoothDampHeight = Mathf.SmoothDamp(
                transform.position.y,    // Current height
                targetHeight,            // Target hover height
                ref turnSmoothVelocity,  // SmoothDamp velocity reference
                turnSmoothTime           // Dampening time
            );

            // Calculate the required vertical velocity for hovering
            float verticalVelocity = smoothDampHeight - transform.position.y;

            // Apply the vertical adjustment using CharacterController.Move
            Vector3 hoverAdjustment = new Vector3(0f, verticalVelocity, 0f);
            controller.Move(hoverAdjustment);
        }
        else
        {
            Debug.LogWarning("No ground detected within raycast range.");
        }
    }

    void movePlayer(Vector3 direction)
    {
        // Calculate the target angle based on input direction and camera orientation
        float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;

        // Smoothly interpolate the player's current Y rotation toward the target angle
        float smoothedAngle = Mathf.SmoothDampAngle(
            transform.eulerAngles.y,
            targetAngle,
            ref turnSmoothVelocity,
            turnSmoothTime
        );

        // Apply the smoothed rotation to the player
        transform.rotation = Quaternion.Euler(0f, smoothedAngle, 0f);

        // Calculate movement direction based on the smoothed angle
        Vector3 moveDirection = Quaternion.Euler(0f, smoothedAngle, 0f) * Vector3.forward;

        // Only apply movement along X and Z
        Vector3 movement = moveDirection.normalized * speed * Time.deltaTime;
        movement.y = 0f; // Ensure no Y-axis movement

        // Move the player using CharacterController
        controller.Move(movement);
    }

}