using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;


public class Movement : MonoBehaviour
{
    public Rigidbody body;
    Vector3 move = Vector3.zero;
 
    public int moveSpeed = 10;

    // Start is called before the first frame update
    void Start()
    {
        print("Bike.");
    }

    // Update is called once per frame
    void Update()
    {

        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");
        

        move = new Vector3(moveX, moveY).normalized;

        body.velocity = new Vector3(move.x * moveSpeed, 0 ,move.y * moveSpeed);

    }
}
