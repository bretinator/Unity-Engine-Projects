using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/*
 * This program handles updating display information.
 * Bret Shepard - 12/19/2022
*/
public class UIController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI populationCounter;
    private PopulationController populationController;

    private void Awake()
    {
        populationController = GameObject.FindGameObjectWithTag("CitizensController").GetComponent<PopulationController>();
        UpdatePopulationCounter();
    }

    public void UpdatePopulationCounter()
    {
        populationCounter.text = "Population: " + populationController.population.ToString();
    }
}
