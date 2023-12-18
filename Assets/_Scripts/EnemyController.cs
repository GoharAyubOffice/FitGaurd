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
    void OnCollisionEnter(Collision collision)
    {
        FoodType foodType = collision.gameObject.GetComponent<FoodType>();
        if (foodType != null)
        {
            switch (foodType.type)
            {
                case FoodType.Type.FastFood:
                    transform.localScale += new Vector3(2f, 2f, 2f); // Increase scale 2x
                    break;
                case FoodType.Type.Salad:
                    transform.localScale -= new Vector3(1f, 1f, 1f); // Maintain the scale
                    break;
                case FoodType.Type.Coke:
                    transform.localScale += new Vector3(1.5f, 1.5f, 1.5f); // Increase scale 1.5x
                    break;
                case FoodType.Type.Junk:
                    transform.localScale -= new Vector3(0.5f, 0.5f, 0.5f); // Decrease scale .5x
                    break;
                case FoodType.Type.Meat:
                    transform.localScale -= new Vector3(3f, 3f, 3f); // Increase scale 3x
                    break;
            }
        }
    }
}