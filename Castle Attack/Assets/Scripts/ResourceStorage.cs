using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Represents a storage system for kingdom resources.
 * Right now the only implemented resource is food.
 * Bret Shepard - 12/19/2022
*/

public class ResourceStorage : MonoBehaviour
{
    private static int FoodCount = 10;

    public static int GetFoodItemCount()
    {
        return FoodCount;
    }

    public static void AddFoodItemCount(int amt)
    {
        int newFoodItemCount = FoodCount + amt;
        if (newFoodItemCount < 0)
        {
            FoodCount = 0;
        }
        else
        {
            FoodCount = newFoodItemCount;
        }
    }
}
