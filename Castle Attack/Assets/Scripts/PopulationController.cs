using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * This program represents the population of the kingdom.
 * Population data and logic are stored here.
 * Bret Shepard - 12/19/2022
*/

public class PopulationController : MonoBehaviour
{
    public int population = 0;
    private UIController uiController;
    private Citizen[] citizens;

    private void Awake()
    {
        uiController = GameObject.FindGameObjectWithTag("UIController").GetComponent<UIController>();
        citizens = new Citizen[population];
        for (int i = 0; i < population; i++)
        {
            citizens[i] = CreateNewCitizen();
        }
    }

    private Citizen CreateNewCitizen()
    {
        return new Citizen();
    }

    public void SubtractCivFromPopulation()
    {
        population -= 1;
        uiController.UpdatePopulationCounter();
    }
}
