using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * This program represents food that citizens eat.
 * Eating food has not been implemented yet.
 * Bret Shepard - 12/18/2022
*/

public class Food : MonoBehaviour
{
    public FoodType foodType;
    public enum FoodType
    {
        Bread,
        Meat,
        Fruit,
        Vegetable
    }

    public void SetFoodType(FoodType _foodItem)
    {
        foodType = _foodItem;
    }

    public FoodType GetFoodType()
    {
        return foodType;
    }
}