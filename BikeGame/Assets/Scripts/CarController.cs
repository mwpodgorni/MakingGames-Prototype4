using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarCheckpoint
{
    Vector3 destination;
    int nextDirection;
    CarCheckpoint(Vector3 dest, int next)
    {
        destination = dest;
        nextDirection = next;
    }
}

public class CarController : MonoBehaviour
{
    public float speed = 12.0f;
     public float rotationSpeed = 5.0f;
    public GameObject carObject;

    public List<string> checkpoints = new List<string>();
    private int currentCheckpointIndex = 0;
    void FixedUpdate()
    {
        if (checkpoints.Count == 0)
        {
            // No checkpoints to follow
            return;
        }
          if (currentCheckpointIndex < checkpoints.Count)
        {
        // Find the GameObject with the current checkpoint name
        GameObject currentCheckpoint = GameObject.Find(checkpoints[currentCheckpointIndex]);

        if (currentCheckpoint != null)
        {
            // Get the position of the current checkpoint
            Vector3 currentCheckpointPosition = currentCheckpoint.transform.position;

            // Set the Y component of the carObject's position to match the checkpoint's Y position
            Vector3 targetPosition = new Vector3(currentCheckpointPosition.x, carObject.transform.position.y, currentCheckpointPosition.z);

            // Calculate the step based on speed and time
            var step = speed * Time.deltaTime;

            // Move the car towards the current checkpoint's X and Z positions
            carObject.transform.position = Vector3.MoveTowards(carObject.transform.position, targetPosition, step);

            // Rotate the carObject to look at the current checkpoint
            Vector3 direction = targetPosition - carObject.transform.position;
            Quaternion rotation = Quaternion.LookRotation(direction);
            carObject.transform.rotation = Quaternion.Slerp(carObject.transform.rotation, rotation, rotationSpeed * Time.deltaTime);

            // Check if the car has reached the current checkpoint
            if (Vector3.Distance(carObject.transform.position, targetPosition) < 0.001f)
            {
                // Move to the next checkpoint or loop back to the first if it's the last one
                currentCheckpointIndex = (currentCheckpointIndex + 1) % checkpoints.Count;
            }
            carObject.transform.position = new Vector3(carObject.transform.position.x, 0.94f, carObject.transform.position.z);
                Quaternion newRotation = Quaternion.Euler(0f, carObject.transform.rotation.eulerAngles.y, 0f);
            carObject.transform.rotation = newRotation;
            
        }
        else
        {
            Debug.LogWarning("Checkpoint not found: " + checkpoints[currentCheckpointIndex]);
            currentCheckpointIndex++; // Move to the next checkpoint if the current one is not found
        }
        }
    }
}
