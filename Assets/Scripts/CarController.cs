using UnityEngine;

public class CarController : MonoBehaviour
{
    public float speed = 20f;
    public float turnSpeed = 100f;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        // Lower center of mass to help stability
        rb.centerOfMass = new Vector3(0, -0.5f, 0);
    }

    void FixedUpdate()
    {
        float moveInput = Input.GetAxis("Vertical");
        float turnInput = Input.GetAxis("Horizontal");

        // Movement: Move the Rigidbody position forward
        Vector3 movement = transform.forward * moveInput * speed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + movement);

        // Steering: Only turn if we are pressing a key
        if (moveInput != 0)
        {
            float turn = turnInput * turnSpeed * Time.fixedDeltaTime;
            Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f);
            rb.MoveRotation(rb.rotation * turnRotation);
        }
    }
}