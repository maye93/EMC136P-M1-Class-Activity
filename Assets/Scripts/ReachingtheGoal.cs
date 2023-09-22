using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReachingtheGoal : MonoBehaviour
{
    public float speed = 5f;
    public float raycastDistance = 1f; // Length of the raycast
    public Transform targetLocation;

    private bool hasReachedTarget = false;

    void Update()
    {
        Vector3 directionToTarget = (targetLocation.position - transform.position).normalized;

        // Perform raycasts in multiple directions
        bool obstacleInFront = Physics.Raycast(transform.position, transform.forward, raycastDistance);
        bool obstacleToLeft = Physics.Raycast(transform.position, -transform.right, raycastDistance);
        bool obstacleToRight = Physics.Raycast(transform.position, transform.right, raycastDistance);

        if (!hasReachedTarget)
        {
            // No obstacle detected, rotate towards the target
            Quaternion targetRotation = Quaternion.LookRotation(directionToTarget);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, speed * Time.deltaTime);

            // Move towards the target
            if (directionToTarget != Vector3.zero)
            {
                transform.Translate(directionToTarget * speed * Time.deltaTime);
            }
        }

        if (obstacleInFront)
        {
            // Rotate away from the obstacle
            Vector3 avoidObstacleDirection = Vector3.Cross(Vector3.up, Vector3.one);
            Quaternion targetRotation = Quaternion.LookRotation(avoidObstacleDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, speed * Time.deltaTime);
        }
        else if (obstacleToLeft && !obstacleToRight)
        {
            // Rotate to the right to avoid the obstacle on the left
            Quaternion targetRotation = Quaternion.LookRotation(transform.right);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, speed * Time.deltaTime);
        }
        else if (!obstacleToLeft && obstacleToRight)
        {
            // Rotate to the left to avoid the obstacle on the right
            Quaternion targetRotation = Quaternion.LookRotation(-transform.right);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, speed * Time.deltaTime);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == targetLocation.gameObject)
        {
            // Cube has collided with the target location, stop the movement
            hasReachedTarget = true;
        }
    }
}