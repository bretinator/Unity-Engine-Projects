using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * This program represents a citizen of the player's kingdom.
 * The citizen gets hungrier over time and eventually dies of starvation.
 * Bret Shepard - 12/18/2022
*/

public class Citizen : MonoBehaviour
{
    private float _hungerMeter = 100.00f;
    public float HungerMeter
    {
        get { return _hungerMeter; }
        set
        {
            if (value > 100.00f) { _hungerMeter = 100.00f; }
            else if (value < 0.00f) { _hungerMeter = 0.00f; }
            else { _hungerMeter = value; }
        }
    }

    private Food[] foods = new Food[8];

    private void Start()
    {
        StartCoroutine(nameof(HungerIncrease));
    }

    private void Update()
    {
        StartCoroutine(nameof(PrintHunger));
    }

    public void Eat(Food food) // Eating has not been implemented yet.
    {
        switch (food.GetFoodType())
        {
            case Food.FoodType.Bread:
                _hungerMeter += 10.00f;
                break;
            case Food.FoodType.Fruit:
                _hungerMeter += 10.00f;
                break;
            case Food.FoodType.Vegetable:
                _hungerMeter += 10.00f;
                break;
            case Food.FoodType.Meat:
                _hungerMeter += 10.00f;
                break;
        }
    }

    public IEnumerator HungerIncrease()
    {
        while (_hungerMeter > 0.00f)
        {
            yield return new WaitForSeconds(1.0f);
            _hungerMeter -= 25.00f;

            if (_hungerMeter == 0.00f)
            {
                Debug.Log("The citizen is hungry.");
                yield return new WaitForSeconds(5.00f);
                Debug.Log("The citizen has died of hunger.");
            }
        }
    }

    private IEnumerator PrintHunger()
    {
        if (_hungerMeter > 0.00f)
        {
            Debug.Log(_hungerMeter);
        }
        
        yield return null;
    }
}
