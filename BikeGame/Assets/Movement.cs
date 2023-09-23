using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;


public class Movement : MonoBehaviour
{
    public float speed = 10.0f;   // Forward speed of the bike
    public float brakePower = 5.0f;  // Deceleration when braking
    public float turnSpeed = 3.0f;  // Turning speed
    public float rotationAngle = 30.0f;  // Rotation angle when turning

    public Transform steeringObject;
    public Transform FrontWheel;
    


    private float currentSpeed = 0.0f;  // Current speed of the bike


    private Rigidbody rb;


    void Start()
    {
        print("Bike.");
        rb = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {

        // Handle input for acceleration
        if (Input.GetKey(KeyCode.W))
        {
            // Accelerate
            currentSpeed = speed;
        }
        else
        {
            // Brake (decelerate)
            currentSpeed = 0.0f;
        }

        // Handle input for braking
        if (Input.GetKey(KeyCode.S))
        {
            // Brake (decelerate faster)
            currentSpeed = -brakePower;
        }

        // Calculate forward force
        Vector3 forwardForce = transform.forward * currentSpeed;

        // Apply forward force
        rb.AddForce(forwardForce);

        // Handle turning input
        float turnInput = 0.0f;
        if (Input.GetKey(KeyCode.D))
        {
            // Turn right
            turnInput = 1.0f;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            // Turn left
            turnInput = -1.0f;
        }

        // Apply turning force
        Vector3 turnTorque = transform.up * turnSpeed * turnInput;
        rb.AddTorque(turnTorque);

        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.up * -rotationAngle * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(Vector3.up * rotationAngle * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            steeringObject.Rotate(Vector3.up * rotationAngle * turnInput * Time.deltaTime);
            FrontWheel.Rotate(Vector3.up * rotationAngle * turnInput * Time.deltaTime);
        }

        if (!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
        {
            // Calculate the rotation needed to reset to the starting position
            float resetRotation = -steeringObject.localEulerAngles.y;

            // Apply the rotation to reset
            steeringObject.Rotate(Vector3.up * resetRotation);
            FrontWheel.Rotate(Vector3.up * resetRotation);
        }

    }
}