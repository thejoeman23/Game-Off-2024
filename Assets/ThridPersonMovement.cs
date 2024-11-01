using UnityEngine;

public class ThridPersonMovement : MonoBehaviour
{
    CharacterController controller;
    [SerializeField] Transform cam;

    [HideInInspector] public bool canMove = true;
    [SerializeField] float speed;
    [Range(0,1)] [SerializeField] float turnSmoothTime;

    float turnSmoothVelocity = 1f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        controller = GetComponent<CharacterController>();
        cam = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(horizontal, 0, vertical).normalized;

        if (direction.magnitude >= .1f)
        {
            Debug.Log("I");

            // the angle that our player needs to be to face the direction we're going ( * mathf.rad2deg just makes it in degrees not radians)
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            // makes the turning smoother
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);

            // not entirely sure what this does im ngl. brakeys am i right?
            Vector3 moveDirection = Quaternion.Euler(0, angle, 0) * Vector3.forward;

            transform.rotation = Quaternion.Euler(0, targetAngle, 0);
            controller.Move(moveDirection.normalized * speed * Time.deltaTime);
        }
    }
}
