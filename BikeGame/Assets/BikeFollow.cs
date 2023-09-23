using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BikeFollow : MonoBehaviour
{
    public Transform target;
    public float pLerp = 0.2f;
    public float rLerp = 0.1f;
    public Vector3 positionOffset = new Vector3(0f, 2f, -5f);
    public Vector3 rotationOffset = new Vector3(0f, 0f, 0f);

    // Update is called once per frame
    void Update()
    {
        Vector3 targetPosition = target.position + positionOffset;
        Quaternion targetRotation = target.rotation * Quaternion.Euler(rotationOffset);

        transform.position = Vector3.Lerp(transform.position, targetPosition, pLerp);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rLerp);
    }
}
