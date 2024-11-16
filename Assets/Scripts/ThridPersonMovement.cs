using UnityEngine;

public class ThridPersonMovement : MonoBehaviour
{
    CharacterController controller;
    [SerializeField] Transform cam;

    [HideInInspector] public bool canMove = true;
    [SerializeField] float speed;
    [Range(0,1)] [SerializeField] float turnSmoothTime;

    float turnSmoothVelocity = 1f;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        cam = Camera.main?.transform;
    }

    void Update()
    {
        var horizontal = Input.GetAxisRaw("Horizontal");
        var vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0, vertical).normalized;

        if (direction.magnitude >= .1f)
        {
            var targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            var angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            Vector3 moveDirection = Quaternion.Euler(0, angle, 0) * Vector3.forward;
            transform.rotation = Quaternion.Euler(0, targetAngle, 0);
            controller.SimpleMove(moveDirection.normalized * (speed * Time.deltaTime));
        }
    }
}