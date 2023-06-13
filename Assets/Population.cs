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
    
    private int generationCycles = 500;
	private int populationSize = 500;
	private int numberOfCarsCrashed = 0;
	private float survivalRate = 0.8f; 
    private float mutationRate = 0.03f;
    private bool crossOver = true;
    private float crossOverPercentageAt = 0.3f;
    public int lengthOfInitialGene = 6;
    private bool weightedParentChoice = true;
    public float penalizeDirectionChange = 0.5f;
    public float penalizeSidewaysChoice = 0f;

    public GameObject successfulIndividium;
    private Car successfulCarController;
    private bool evolutionSuccessful = false;

    void Start()
    {
        for (int i = 0; i < populationSize; i++)
        {
            
            // Create the initial geneOfIndividual of the car 
            List<Car.DirectionsEnum> initialGene = new List<Car.DirectionsEnum>();
            for (int j = 0; j < lengthOfInitialGene; j++)
            {
                initialGene.Add((Car.DirectionsEnum)Random.Range(0, 3));
            }

            // Spawn cars on starting point for every population
            GameObject car = Instantiate(carPrefab, this.transform.position, this.transform.rotation);
            Car carController = car.GetComponent<Car>();
            //Debug.Log("INITAL GENE should be 0 and is ... " + carController.getGeneString().Count());
            carController.setInheritedGenes(initialGene);
            //Debug.Log("INITAL GENE should be " + lengthOfInitialGene + " and is ... " + carController.getGeneString().Count());
            cars.Add(car);
            carControllers.Add(carController);
        }
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
                    //Debug.Log("Cars crashed " + numberOfCarsCrashed);
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
            List<Car> individualsToBeParents = e.sortAndSelectCarsForFitness(carControllers, survivalRate, penalizeSidewaysChoice, penalizeDirectionChange);
            Debug.Log("Sorted fittest survivers: " + individualsToBeParents.Count());
            
            //create new population
            for (int i = 0; i < populationSize; i++)
            {
                Car parent1 = e.selectParent(individualsToBeParents, weightedParentChoice);
                Car parent2 = e.selectParent(individualsToBeParents, weightedParentChoice);
                List<Car.DirectionsEnum> geneOfChildCar = e.makeChild(parent1, parent2, mutationRate, crossOver, crossOverPercentageAt);
                
                // Spawn cars on starting point for every population
                GameObject childCar = Instantiate(carPrefab, this.transform.position, this.transform.rotation);
                Car carController = childCar.GetComponent<Car>();   
                carController.setInheritedGenes(geneOfChildCar);
                newCars.Add(childCar);
                newCarControllers.Add(carController);
            }

            // Clean and setup for next generation
            foreach (var item in cars)
            {
                Destroy(item);
            }
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
