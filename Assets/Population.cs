using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Linq;

public class Population : MonoBehaviour
{

    public GameObject carPrefab;
    public List<GameObject> cars;
    public List<Car> carControllers;

	int numberCars = 5;
	int numberOfCarsCrashed = 0;
	int survivalRate = 2;
    int numberCars = 5;
    int numberOfCarsCrashed = 0;
    int survivalRate = 2;

    void Start()
    {
        for (int i = 0; i < numberCars; i++)
        {
            //TODO: Spawn parallel e.g. Randomize start position 
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

            //sort/choose cars depending on their fitness value
            //Debug.Log("Sorted after fitness: " + sortAndSelectCarsForFitness(carControllers)[0].distance);
            sortAndSelectCarsForFitness(carControllers);
            Debug.Log("Sorted survivers: " + sortAndSelectCarsForFitness(carControllers).Count());

            makeChild(sortAndSelectCarsForFitness(carControllers)[0], sortAndSelectCarsForFitness(carControllers)[1]);

        }

    }



    List<Car> sortAndSelectCarsForFitness(List<Car> cars)
    {
        List<Car> sortedCarsAfterFitness = cars.OrderBy(car => car.distance).ToList();
        List<Car> surviverCars = sortedCarsAfterFitness.Take(survivalRate).ToList();
        return surviverCars;
    }


    Car makeChild(Car parent1, Car parent2)
    {
        List<Car.DirectionsEnum> mom = parent1.direction;
        List<Car.DirectionsEnum> dad = parent2.direction;
        List<Car.DirectionsEnum> child = new List<Car.DirectionsEnum>();
        Debug.Log("MAKEE SOMETHING!!");

		

        //TODO: step < mom.Count()
        for (int step = 0; step < 1; step++)
        {

            int selectedParent = Random.Range(0, 2);
            Debug.Log("My parent is: " + selectedParent);
            switch (selectedParent)
            {
                case 0:
                    Debug.Log("Mom!");
                    //child.Add((Car.DirectionsEnum) mom.direction[step]);
                    break;
                case 1:
                    Debug.Log("Dad!");
                    //child.Add((Car.DirectionsEnum) dad.direction[step]);
                    break;
            }
        }
        Car childCar = new Car();
        //childCar.direction = child;
        return childCar;
    }
}
