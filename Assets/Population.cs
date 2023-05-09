using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Population : MonoBehaviour
{

    public GameObject carPrefab;
    public List<GameObject> cars;
    public List<Car> newCarControllers;
    public List<Car> carControllers;

	int numberCars = 5;
	int numberOfCarsCrashed = 0;
	int survivalRate = 2;
    float mutationRate = 0.93f;

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

            //sort/choose cars depending on their fitness value
            //Debug.Log("Sorted after fitness: " + sortAndSelectCarsForFitness(carControllers)[0].distance);
            sortAndSelectCarsForFitness(carControllers);
            Debug.Log("Sorted survivers: " + sortAndSelectCarsForFitness(carControllers).Count());

            for (int i = 0; i < cars.Count(); i++)
            {
                Car childCar = makeChild(sortAndSelectCarsForFitness(carControllers)[0], sortAndSelectCarsForFitness(carControllers)[1]);
                newCarControllers.Add(childCar);
            }

            carControllers.Clear();
            carControllers.AddRange(newCarControllers);
            Debug.Log("Number of cars " + numberCars + " new cars " + carControllers.Count());

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

        List<Car.DirectionsEnum> longerParent = new List<Car.DirectionsEnum>();
        int commonGeneLength = 0;

        if (mom.Count() > dad.Count()) {
            longerParent = mom;
            commonGeneLength = dad.Count();
        } else {
            longerParent = dad;
            commonGeneLength = mom.Count();
        }
		

        for (int step = 0; step < commonGeneLength; step++)
        {

            int selectedParent = Random.Range(0, 2);
            Debug.Log("My parent is: " + selectedParent);
            switch (selectedParent)
            {
                case 0:
                    Debug.Log("Mom!");
                    child.Add(mom[step]);
                    break;
                case 1:
                    Debug.Log("Dad!");
                    child.Add(dad[step]);
                    break;
            }
        }

        for (int i = commonGeneLength; i < longerParent.Count(); i++) {
            child.Add(longerParent[i]);
        }

        Debug.Log("Child has gene length of " + child.Count() + " and should have length of " + longerParent.Count());

        Car childCar = new Car();
        
        childCar.direction = MutateGene(child);
        return childCar;
    }


    List<Car.DirectionsEnum> MutateGene(List<Car.DirectionsEnum> gene)
    {

        List<Car.DirectionsEnum> mutatedGene = new List<Car.DirectionsEnum>();
        Debug.Log("Mutation.....................................................................................");
        for (int i = 0; i < gene.Count(); i++)
        {
            System.Random random = new System.Random();
            if (random.NextDouble() < survivalRate)
            {
                Debug.Log("Mutation takes effect at " + i + " in gene.");
                mutatedGene.Add((Car.DirectionsEnum)Random.Range(0, 3));
            } else
            {
                mutatedGene.Add(gene[i]);
            }
        }
        return mutatedGene;
    }
}
