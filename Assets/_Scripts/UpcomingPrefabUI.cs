using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpcomingPrefabUI : MonoBehaviour
{
    public FoodThrower foodThrower; // Reference to the FoodThrower script
    public TextMeshProUGUI upcomingPrefabText; // TextMeshProUGUI instead of Text

    void Update()
    {
        // Get the name of the upcoming prefab
        string upcomingPrefabName = foodThrower.GetUpcomingPrefabName();
        // Remove "(Clone)" from the name
        upcomingPrefabName = upcomingPrefabName.Replace("(Clone)", "");
        // Update the UI text field
        upcomingPrefabText.text = "Upcoming Food: " + upcomingPrefabName;
    }
}
