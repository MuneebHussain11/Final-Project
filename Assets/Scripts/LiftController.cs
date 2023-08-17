using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiftController : MonoBehaviour
{
    public float moveSpeed = 2.0f;
    public float moveDistance = 5.0f;
    public bool isMoving = true;

    private Vector3 initialPosition;
    private Vector3 targetPosition;

    private void Start()
    {
        initialPosition = transform.position;
        targetPosition = initialPosition + new Vector3(moveDistance, 0.0f, 0.0f);
    }




    // Update is called once per frame
    private void Update()
    {
        if (isMoving)
        {
            Vector3 newPosition = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            transform.position = newPosition;

            if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
            {
                targetPosition = targetPosition == initialPosition ? initialPosition + new Vector3(moveDistance, 0.0f, 0.0f) : initialPosition;
            }
        }
    }
}
