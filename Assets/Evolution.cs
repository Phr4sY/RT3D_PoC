using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Evolution 
{

    public Evolution() {

    }

    public List<Car> sortAndSelectCarsForFitness(List<Car> cars, float survivalRate)
    {
        int numberOfFittestIndividuals = (int) (survivalRate * cars.Count());
        Debug.Log("Number of fittest individuals is " + numberOfFittestIndividuals);
        List<Car> sortedCarsAfterFitness = cars.OrderBy(car => car.distance).ToList();
        List<Car> fittestSurviverCars = sortedCarsAfterFitness.Take(numberOfFittestIndividuals).ToList();
        return fittestSurviverCars;
    }


    public Car makeChild(Car parent1, Car parent2, float mutationRate)
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


    public List<Car.DirectionsEnum> MutateGene(List<Car.DirectionsEnum> gene, float mutationRate)
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
