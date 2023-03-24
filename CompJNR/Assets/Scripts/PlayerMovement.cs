using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController charController;

    public float speed = 12.0f;
    public float maxSpeed = 24f;
    public float jumpHeight = 3f;

    public float g = -10;

    public Transform groundCheck;
    public float groundDist = 0.4f;
    public LayerMask groudMask;


    Vector3 v;
    bool isGrounded;

    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDist, groudMask);

        if (isGrounded && v.y < 0)
        {
            v.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");



        Vector3 move = transform.right * x + transform.forward * z;

        if (Input.GetKey(KeyCode.LeftShift))
        {
            charController.Move(move * maxSpeed * Time.deltaTime);
        }
        else
        {
            charController.Move(move * speed * Time.deltaTime);
        }

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            v.y = Mathf.Sqrt(jumpHeight * -2f * g);
        }


        v.y += g * Time.deltaTime;

        charController.Move(v * Time.deltaTime);

    }
}