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

            //sort/choose cars depending on their fitness value
            List<Car> individualsToBeParents = sortAndSelectCarsForFitness(carControllers, survivalRate);
            Debug.Log("Sorted fittest survivers: " + individualsToBeParents.Count());

            //create new population
            for (int i = 0; i < populationSize; i++)
            {
                int selectedParent1 = Random.Range(0, individualsToBeParents.Count());
                int selectedParent2 = Random.Range(0, individualsToBeParents.Count());
                Debug.Log("Choose parent number " + selectedParent1 + " and " + selectedParent2);
                Car childCar = makeChild(individualsToBeParents[selectedParent1], individualsToBeParents[selectedParent2], mutationRate);
                newCarControllers.Add(childCar);
            }

            carControllers.Clear();
            carControllers.AddRange(newCarControllers);
            Debug.Log("Size of population " + populationSize + " and number of new cars " + carControllers.Count());
        }

    }



    List<Car> sortAndSelectCarsForFitness(List<Car> cars, float survivalRate)
    {
        int numberOfFittestIndividuals = (int) (survivalRate * cars.Count());
        Debug.Log("Number of fittest individuals is " + numberOfFittestIndividuals);
        List<Car> sortedCarsAfterFitness = cars.OrderBy(car => car.distance).ToList();
        List<Car> fittestSurviverCars = sortedCarsAfterFitness.Take(numberOfFittestIndividuals).ToList();
        return fittestSurviverCars;
    }


    Car makeChild(Car parent1, Car parent2, float mutationRate)
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
            switch (selectedParent)
            {
                case 0:
                    Debug.Log("My favourite parent is Mom!");
                    child.Add(mom[step]);
                    break;
                case 1:
                    Debug.Log("My favourite parent is Dad!");
                    child.Add(dad[step]);
                    break;
            }
        }

        for (int i = commonGeneLength; i < longerParent.Count(); i++) {
            child.Add(longerParent[i]);
        }

        Debug.Log("Child has gene length of " + child.Count() + " and should have length of " + longerParent.Count());

        Car childCar = new Car();
        
        childCar.direction = MutateGene(child, mutationRate);
        return childCar;
    }


    List<Car.DirectionsEnum> MutateGene(List<Car.DirectionsEnum> gene, float mutationRate)
    {
        List<Car.DirectionsEnum> mutatedGene = new List<Car.DirectionsEnum>();
        for (int i = 0; i < gene.Count(); i++)
        {
            System.Random random = new System.Random();
            if (random.NextDouble() < mutationRate)
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
