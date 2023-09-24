using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VerticalAxisBikerMovement : MonoBehaviour
{
    [SerializeField] WheelCollider frontWheel;
    [SerializeField] Transform frontWheelTransform;
    public float maxTurnAngle = 15f;
    public float maxSpeed = 10f;
    public float acceleration = 1f;
    public float maxRollAngle = 10f;
    public float rollSpeed = 2f;

    private Rigidbody rb;
    private float currentTurnAngle = 0f;
    private float currentSpeed = 0f;
    private float currentRollAngle = 0f;
    private float initialYPosition;
    private bool hitCar = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        initialYPosition = transform.position.y;
    }

    private void Update()
    {
        if (!hitCar)
        {
            // Rotate the entire character on the y-axis to turn
            currentTurnAngle = maxTurnAngle * Input.GetAxis("Horizontal");
            transform.Rotate(Vector3.up * currentTurnAngle * Time.deltaTime);

            // Ensure the character's Y position stays the same
            Vector3 newPosition = transform.position;
            // newPosition.y = initialYPosition;
            transform.position = newPosition;

            // Use the vertical axis input for speed control
            float verticalInput = Input.GetAxis("Vertical");

            // limit the rotation on the z-axis
            currentRollAngle = Mathf.Clamp(currentRollAngle, -maxRollAngle, maxRollAngle);
            Vector3 currentRotation = transform.rotation.eulerAngles;
            currentRotation.z = currentRollAngle; // Set z-axis rotation to the current roll angle
            transform.rotation = Quaternion.Euler(currentRotation);

            // Calculate the desired speed based on vertical input
            float desiredSpeed = maxSpeed * verticalInput;

            // Accelerate or decelerate based on the desired speed
            currentSpeed = Mathf.MoveTowards(currentSpeed, desiredSpeed, acceleration * Time.deltaTime);
            currentSpeed = Mathf.Max(currentSpeed, -5f);

            // Move the character based on the current speed
            rb.velocity = transform.forward * currentSpeed;

            // Smoothly roll back to the allowed range
            currentRollAngle = Mathf.MoveTowards(currentRollAngle, 0f, rollSpeed * Time.deltaTime);
            // UpdateWheel(frontWheel, frontWheelTransform);
        }

    }

    void UpdateWheel(WheelCollider col, Transform trans)
    {
        Vector3 position;
        Quaternion rotation;

        col.GetWorldPose(out position, out rotation);
        // Vector3 adjustedPosition = new Vector3(position.x + adjustPosition.x, position.y + adjustPosition.y, position.z + adjustPosition.z);
        // Quaternion adjustedRotation = Quaternion.Euler(rotation.eulerAngles.x, rotation.eulerAngles.y, rotation.eulerAngles.z + 90f);
        trans.position = position;
        trans.rotation = rotation;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("car"))
        {
            Debug.Log("Collision with a car!");
            hitCar = true;
            StartCoroutine(LoadSceneAfterDelay(3f));

        }
    }
    private IEnumerator LoadSceneAfterDelay(float delayInSeconds)
    {
        yield return new WaitForSeconds(delayInSeconds);
        PlayerPrefs.SetString("endText", "Game Over");
        SceneManager.LoadScene(2);
    }
}
