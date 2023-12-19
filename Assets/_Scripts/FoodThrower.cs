using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodThrower : MonoBehaviour
{
    public List<GameObject> foodPrefabs;
    public Transform shootPosition;
    public float throwForce = 700f;
    public float destroyTime = 5f;
    public float tapDelay = 1f; // Delay between taps
    public int poolSize = 10;

    private List<GameObject> objectPool;
    private int currentPoolIndex = 0;
    private float lastTapTime; // Time of the last tap

    void Start()
    {
        // Initialize the object pool
        objectPool = new List<GameObject>();
        for (int i = 0; i < poolSize; i++)
        {
            int index = i % foodPrefabs.Count; // Cycle through the foodPrefabs list
            GameObject food = Instantiate(foodPrefabs[index], shootPosition.position, Quaternion.identity);
            food.SetActive(false);
            food.AddComponent<Food>(); // Add the Food component to each food prefab
            objectPool.Add(food);
        }
    }

    void Update()
    {
        // Check for a screen tap
        if (Input.GetMouseButtonDown(0) && Time.time - lastTapTime >= tapDelay)
        {
            ThrowFoodOnce();
            lastTapTime = Time.time; // Update the time of the last tap
        }
    }

    public string GetUpcomingPrefabName()
    {
        // Get the name of the upcoming prefab
        return objectPool[currentPoolIndex].name;
    }

    void ThrowFoodOnce()
    {
        GameObject food = GetPooledObject();
        if (food != null)
        {
            food.transform.position = shootPosition.position;
            food.SetActive(true);
            Debug.Log("Instantiated prefab: " + food.name); // Log the name of the instantiated prefab
            Rigidbody rb = food.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.velocity = Vector3.zero; // Reset the velocity
                rb.AddForce(shootPosition.up * throwForce);
            }
            StartCoroutine(DestroyAfterTime(food, destroyTime));
            StartCoroutine(CheckCollision(food, destroyTime)); // Start the CheckCollision coroutine

            // Cycle through the object pool
            currentPoolIndex = (currentPoolIndex + 1) % objectPool.Count;
        }
    }

    GameObject GetPooledObject()
    {
        // Start searching from the current pool index
        for (int i = currentPoolIndex; i < objectPool.Count; i++)
        {
            if (!objectPool[i].activeInHierarchy)
            {
                return objectPool[i];
            }
        }

        // If no inactive object is found, return null
        return null;
    }

    IEnumerator DestroyAfterTime(GameObject food, float delay)
    {
        yield return new WaitForSeconds(delay);
        food.SetActive(false);
    }

    IEnumerator CheckCollision(GameObject food, float delay)
    {
        yield return new WaitForSeconds(delay);

        // If the food hasn't hit an enemy within the time limit
        if (!food.GetComponent<Food>().hitEnemy)
        {
            ScoreManager.DecreaseScore(1); // Decrease score by 1
        }

        // Reset the hitEnemy flag
        food.GetComponent<Food>().hitEnemy = false;
    }
}

public class Food : MonoBehaviour
{
    public bool hitEnemy = false;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("enemy"))
        {
            hitEnemy = true; // Set hitEnemy to true when the food hits an enemy
        }
    }
}