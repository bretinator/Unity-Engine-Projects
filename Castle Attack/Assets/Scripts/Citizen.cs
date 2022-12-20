using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Represents a citizen of the player's kingdom.
 * The citizen gets hungrier over time and eventually dies of starvation.
 * Bret Shepard - 12/18/2022
*/

public class Citizen : MonoBehaviour
{
    private bool isRunning = true;
    private bool isEating = false;

    private PopulationController populationController;
    public float _hungerMeter = 100.00f;
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

    private void Awake()
    {
        populationController = GameObject.FindGameObjectWithTag("CitizensController").GetComponent<PopulationController>();
    }

    private void Start()
    {
        StartCoroutine(nameof(PrintHunger));
        StartCoroutine(nameof(HungerIncrease));
    }

    private void Update()
    {
        if (HungerMeter == 0.00f && !isEating) // Starts eating when hunger is at minimum amount.
        {
            Eat();
        }
    }

    public void Eat() // Eats until full or until food runs out.
    {
        isEating = true;
        while (HungerMeter < 100.00f && ResourceStorage.GetFoodItemCount() > 0)
        {
            ResourceStorage.AddFoodItemCount(-1);
            HungerMeter += 10.00f;
        }
        isEating = false;

        if (HungerMeter > 0.00f)
        {
            StartCoroutine(nameof(HungerIncrease));
        }
    }

    public void Die()
    {
        isRunning = false;
        populationController.SubtractCivFromPopulation();
    }

    public IEnumerator HungerIncrease()
    {
        while (HungerMeter > 0.00f && !isEating)
        {
            yield return new WaitForSeconds(1.0f);
            HungerMeter -= 25.00f;
        }
    }

    private IEnumerator PrintHunger()
    {
        while (isRunning)
        {
            if (HungerMeter > 0.00f)
            {
                Debug.Log(HungerMeter);
                yield return new WaitForSecondsRealtime(0.5f);
            }
            else
            {
                yield return null;
            }
        }
    }
}
