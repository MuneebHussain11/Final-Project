using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goomba : MonoBehaviour
{
    public float moveSpeed = 2.0f;
    public float patrolDistance = 3.0f; // Distance the Goomba will patrol

    private Rigidbody rb;
    private Vector3 initialPosition;
    private bool isMovingRight = true;
    private Vector3 patrolStartPosition;
    public Vector3 rotation;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        initialPosition = rb.position;
        patrolStartPosition = initialPosition;
    }


    // Start is called before the first frame update



    // Update is called once per frame
    private void FixedUpdate()
    {
        // Determine the movement direction based on whether Goomba is moving right or left
        Vector3 movement = isMovingRight ? Vector3.left : Vector3.right;

        // Apply movement using velocity
        rb.velocity = movement * moveSpeed;

        // Calculate the distance between current position and patrol start position
        float distanceFromStart = Vector3.Distance(rb.position, patrolStartPosition);

        // Check if Goomba needs to change direction
        if (distanceFromStart >= patrolDistance)
        {
            isMovingRight = !isMovingRight;
            patrolStartPosition = rb.position; // Update the start position
        }
        //transform.Rotate(Vector3.right * 5 * Time.deltaTime);
    }
    
}
