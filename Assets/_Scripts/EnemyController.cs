using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed = 1f;
    public float leftLimit = -4.5f;
    public float rightLimit = 4.5f;
    public float xOffsetChangeSpeed = 0.5f;
    public float destroyPositionZ = -10f;
    private float individualDestroyPositionZ;
    private bool stopMovement = false;
    private float targetX;
    public float minSpeed, maxSpeed;
    public bool hitEnemy = false;
    public float minXOffsetChangeSpeed, maxXOffsetChangeSpeed;


    public static List<float> stoppedEnemyPositions = new List<float>();

    void Start()
    {
        individualDestroyPositionZ = destroyPositionZ;
        speed = Random.Range(minSpeed, maxSpeed);
        xOffsetChangeSpeed = Random.Range(minXOffsetChangeSpeed, maxXOffsetChangeSpeed);
        targetX = rightLimit;
    }

    void Update()
    {
        if (!stopMovement)
        {
            MoveForward();

            if (stoppedEnemyPositions.Contains(individualDestroyPositionZ))
            {
                individualDestroyPositionZ -= 1f;
            }

            if (transform.position.z <= individualDestroyPositionZ)
            {
                stopMovement = true;
                speed = 0;
                stoppedEnemyPositions.Add(individualDestroyPositionZ);
            }
        }
    }

    private void MoveForward()
    {
        float randomZOffset = Random.Range(-0.1f, 0.1f);
        transform.position += -Vector3.forward * (speed + randomZOffset) * Time.deltaTime;

        float newX = Mathf.Lerp(transform.position.x, targetX, xOffsetChangeSpeed * Time.deltaTime);
        transform.position = new Vector3(newX, transform.position.y, transform.position.z);

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
        if (collision.gameObject.CompareTag("Foods"))
        {
            ScoreManager.IncreaseScore(100); // Increase score by 1 when your prefab hits an enemy
        }
        FoodType foodType = collision.gameObject.GetComponent<FoodType>();
        if (foodType != null)
        {
            Vector3 scaleChange;
            switch (foodType.type)
            {
                case FoodType.Type.FastFood:
                    scaleChange = new Vector3(2f, 2f, 2f);
                    break;
                case FoodType.Type.Salad:
                    scaleChange = new Vector3(-1f, -1f, -1f);
                    break;
                case FoodType.Type.Coke:
                    scaleChange = new Vector3(1.5f, 1.5f, 1.5f);
                    break;
                case FoodType.Type.Junk:
                    scaleChange = new Vector3(-0.5f, -0.5f, -0.5f);
                    break;
                case FoodType.Type.Meat:
                    scaleChange = new Vector3(-3f, -3f, -3f);
                    break;
                default:
                    scaleChange = Vector3.zero;
                    break;
            }

            // Check if the new scale is within the limits
            Vector3 newScale = transform.localScale + scaleChange;
            if (newScale.x >= 0.5f && newScale.y >= 0.5f && newScale.z >= 0.5f &&
                newScale.x <= 3f && newScale.y <= 3f && newScale.z <= 3f)
            {
                // Apply the scale change
                transform.localScale = newScale;
            }
        }
    }
    void OnDestroy()
    {
        if (!hitEnemy) // If the prefab is destroyed without hitting an enemy
        {
            ScoreManager.DecreaseScore(100); // Decrease score by 1
        }
    }
}