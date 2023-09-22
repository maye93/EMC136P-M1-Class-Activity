using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StraightMovement : MonoBehaviour
{
    public Vector3 direction = Vector3.forward;
    public float speed = 5f;

    public Transform targetLocation;

    private bool hasReachedTarget = false;

    void Update()
    {
        if (!hasReachedTarget)
        {
            Vector3 directionToTarget = (targetLocation.position - transform.position).normalized;
            transform.Translate(direction * speed * Time.deltaTime);
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
