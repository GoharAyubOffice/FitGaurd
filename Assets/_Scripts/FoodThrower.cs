using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodThrower : MonoBehaviour
{
    private int currentPoolIndex = 0; // Add this line at the top of your class
    public List<GameObject> foodPrefabs;
    public float throwForce = 1000f;
    public float delay = 1f;
    public Transform shootPosition;
    public int poolSize = 20; // Size of the object pool
    public float destroyTime = 5f; // Time after which the objects will be destroyed

    private List<GameObject> objectPool; // The object pool

    void Start()
    {
        // Initialize the object pool
        objectPool = new List<GameObject>();
        for (int i = 0; i < poolSize; i++)
        {
            int index = i % foodPrefabs.Count; // Cycle through the foodPrefabs list
            GameObject food = Instantiate(foodPrefabs[index], shootPosition.position, Quaternion.identity);
            food.SetActive(false);
            objectPool.Add(food);
        }

        StartCoroutine(StartThrowing());
    }
    IEnumerator StartThrowing()
    {
        yield return new WaitForSeconds(1f); // Wait for 1 second
        StartCoroutine(ThrowFood());
    }
    IEnumerator ThrowFood()
{
    while (true)
    {
        GameObject food = GetPooledObject();
        if (food != null)
        {
            food.transform.position = shootPosition.position;
            food.transform.SetParent(shootPosition); // Set the parent to the weapon
            food.SetActive(true);
            food.transform.parent = null; // Remove the parent
            Debug.Log("Instantiated prefab: " + food.name); // Log the name of the instantiated prefab
            Rigidbody rb = food.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.velocity = Vector3.zero; // Reset the velocity
                rb.AddForce(shootPosition.up * throwForce);
            }
            StartCoroutine(DestroyAfterTime(food, destroyTime));
        }
        yield return new WaitForSeconds(delay);

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

    IEnumerator DestroyAfterTime(GameObject obj, float time)
    {
        yield return new WaitForSeconds(time);
        obj.SetActive(false);
    }
}