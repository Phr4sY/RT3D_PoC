using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class Population : MonoBehaviour
{
    public Evolution e = new Evolution();
    public GameObject carPrefab;
    public List<GameObject> cars;
    public List<Car> newCarControllers;
    public List<Car> carControllers;

	private int populationSize = 5;
	private int numberOfCarsCrashed = 0;
	private float survivalRate = 0.5f;
    private float mutationRate = 0.13f;

    void Start()
    {
        for (int i = 0; i < populationSize; i++)
        {
            //TODO: Spawn cars on starting point for every population
            GameObject car = Instantiate(carPrefab, this.transform.position, this.transform.rotation);
            Car carController = car.GetComponent<Car>();
            cars.Add(car);
            carControllers.Add(carController);
        }
        Debug.Log(cars[0].GetComponent<Car>().distance);
    }

    void Update()
    {
        Debug.Log(carControllers[0].distance);

        numberOfCarsCrashed = 0;

        for (int i = 0; i < carControllers.Count; i++)
        {
            if (carControllers[i].crashed)
            {
                numberOfCarsCrashed++;
            }
        }

        //check if all cars died
        if (numberOfCarsCrashed == cars.Count)
        {
            Debug.Log("All cars crashed. Yay");
            Evolution e = new Evolution();
            //sort/choose cars depending on their fitness value
            List<Car> individualsToBeParents = e.sortAndSelectCarsForFitness(carControllers, survivalRate);
            Debug.Log("Sorted fittest survivers: " + individualsToBeParents.Count());

            //create new population
            for (int i = 0; i < populationSize; i++)
            {
                int selectedParent1 = Random.Range(0, individualsToBeParents.Count());
                int selectedParent2 = Random.Range(0, individualsToBeParents.Count());
                Debug.Log("Choose parent number " + selectedParent1 + " and " + selectedParent2);
                Car childCar = e.makeChild(individualsToBeParents[selectedParent1], individualsToBeParents[selectedParent2], mutationRate);
                newCarControllers.Add(childCar);
            }

            carControllers.Clear();
            carControllers.AddRange(newCarControllers);
            Debug.Log("Size of population " + populationSize + " and number of new cars " + carControllers.Count());
        }

    }

}
