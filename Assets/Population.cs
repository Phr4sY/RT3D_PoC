using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class Population : MonoBehaviour
{
    public Evolution e = new Evolution();
    public GameObject carPrefab;
    public List<GameObject> cars;
    public List<GameObject> newCars;
    public List<Car> newCarControllers;
    public List<Car> carControllers;
    
    private int generationCycles = 5;
	private int populationSize = 4;
	private int numberOfCarsCrashed = 0;
	private float survivalRate = 0.5f; 
    private float mutationRate = 0.13f;

    public GameObject successfulIndividium;
    private Car successfulCarController;
    private bool evolutionSuccessful = false;

    void Start()
    {
        for (int i = 0; i < populationSize; i++)
        {
            
            // Create the initial geneOfIndividual of the car 
            List<Car.DirectionsEnum> initialGene = new List<Car.DirectionsEnum>();
            for (int j = 0; j < 2; j++)
            {
                initialGene.Add((Car.DirectionsEnum)Random.Range(0, 3));
                Debug.Log(initialGene[j]);
            }

            // Spawn cars on starting point for every population
            GameObject car = Instantiate(carPrefab, this.transform.position, this.transform.rotation);
            Car carController = car.GetComponent<Car>();
            carController.setInheritedGenes(initialGene);
            cars.Add(car);
            carControllers.Add(carController);
        }
        Debug.Log(cars[0].GetComponent<Car>().distance);
        generationCycles--;
    }

    void Update()
    {
        if (!evolutionSuccessful) {

            numberOfCarsCrashed = 0;

            for (int i = 0; i < carControllers.Count(); i++)
            {
                if (carControllers[i].crashed)
                {
                    numberOfCarsCrashed++;
                }

                // check if an individual has successfully reached target
                if (carControllers[i].targetReached) {
                    Debug.Log("The evolution was successful!!!!! The individual nr. " + i + " has reached the target.");
                    successfulIndividium = cars[i];
                    successfulCarController = carControllers[i]; 
                    evolutionSuccessful = true;
                }
            }
        }

        

        // check if all cars died
        if ((numberOfCarsCrashed == cars.Count) && (generationCycles > 0)) 
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
                List<Car.DirectionsEnum> geneOfChildCar = e.makeChild(individualsToBeParents[selectedParent1], individualsToBeParents[selectedParent2], mutationRate);

                // Spawn cars on starting point for every population
                GameObject childCar = Instantiate(carPrefab, this.transform.position, this.transform.rotation);
                Car carController = childCar.GetComponent<Car>();   
                carController.setInheritedGenes(geneOfChildCar);
                newCars.Add(childCar);
                newCarControllers.Add(carController);
            }

            // Clean and setup for next generation
            carControllers.Clear();
            carControllers.AddRange(newCarControllers);
            newCarControllers.Clear();
            cars.Clear();
            cars.AddRange(newCars);
            newCars.Clear();
            Debug.Log("Size of population " + populationSize + " and number of new cars " + carControllers.Count());

            generationCycles--;
            Debug.Log(generationCycles + " generations to go.");
        } 
    }
}
