using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodCollision : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        // Check if the collided object is an enemy
        if (collision.gameObject.CompareTag("enemy"))
        {
            // Deactivate the prefab
            gameObject.SetActive(false);
        }
    }
}