using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EnemyController : MonoBehaviour
{
    public float speed = 1f;
    public float leftLimit = -4.5f;
    public float rightLimit = 4.5f;
    public float xOffsetChangeSpeed = 0.5f;
     public float destroyPositionZ = -10f; // Add this line
    private float targetX;

    void Start()
    {
        targetX = rightLimit; // Initially set the target x position to the right limit
    }


    void Update()
    {
        MoveForward();

        // Destroy the enemy if it moves through the specific position
        if (transform.position.z <= destroyPositionZ)
        {
            Destroy(gameObject);
        }
    }

    private void MoveForward()
    {
        // Move forward
        transform.position += -Vector3.forward * speed * Time.deltaTime;

        // Smoothly change the x position
        float newX = Mathf.Lerp(transform.position.x, targetX, xOffsetChangeSpeed * Time.deltaTime);
        transform.position = new Vector3(newX, transform.position.y, transform.position.z);

        // Change the target x position when the enemy reaches the left or right limit
        if (Mathf.Abs(transform.position.x - rightLimit) < 0.1f)
        {
            targetX = leftLimit;
        }
        else if (Mathf.Abs(transform.position.x - leftLimit) < 0.1f)
        {
            targetX = rightLimit;
        }
    }
}