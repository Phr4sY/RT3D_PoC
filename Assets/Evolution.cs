using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Evolution 
{

    public Evolution() {

    }

    public List<Car> sortAndSelectCarsForFitness(List<Car> cars, float survivalRate, float penaltyForDirectionChange, float penaltyForDrivingSideways)
    {
        int numberOfFittestIndividuals = (int) (survivalRate * cars.Count());
        if (numberOfFittestIndividuals < 1) {
            Debug.Log("Survival rate is too small. Survival rate is set to 1.");
            numberOfFittestIndividuals = 1;
        }
        Debug.Log("Number of fittest individuals is " + numberOfFittestIndividuals);

        List<Car> sortedCarsAfterFitness = new List<Car>();
      /*  if (punishSidewaysChoice) {
            float penalty = 0.5f;
            sortedCarsAfterFitness = sortingCarsByPunishingSidewayChoices(cars, penalty);
        } 
        if (punishDirectionChange) {
            float penaltyScalerDirectionChange = 0.7f;
            sortedCarsAfterFitness = sortingCarsByPunishingDirectionChange(cars, penaltyScalerDirectionChange);
        } else {
            sortedCarsAfterFitness = cars.OrderByDescending(car => car.getFitnessValue()).ToList();
        }*/

        sortedCarsAfterFitness = sortingCarsByPunishingFeatures(cars, penaltyForDirectionChange, penaltyForDrivingSideways);

        //Debug.Log("Fitness of cars on first places:" + sortedCarsAfterFitness[0].getFitnessValue() + " & " + sortedCarsAfterFitness[1].getFitnessValue());
        List<Car> fittestSurviverCars = sortedCarsAfterFitness.Take(numberOfFittestIndividuals).ToList();
        return fittestSurviverCars;
    }

    private List<Car> sortingCarsByPunishingDirectionChange(List<Car> cars, float penalty) {
        List<Car> carsWithWeightedFitness = new List<Car>();
        foreach (var car in cars)
        {
            float newFitnessValue = 0;
            for (int genePart = 0; genePart < car.getFitnessValue() - 1; genePart++) {
                if (car.getGeneString()[genePart] == car.getGeneString()[genePart+1]) {
                    //Debug.Log("Those two directions should be the same: " + car.getGeneString()[genePart] + " & " + car.getGeneString()[genePart+1]);
                    newFitnessValue++;
                } else {
                    newFitnessValue = newFitnessValue + 1 * penalty;
                }
            }
            Debug.Log("Instead of getting " + car.getFitnessValue() + " its new value is " + newFitnessValue);
            car.setFitnessValue(newFitnessValue);
            Debug.Log("New value is " + car.getFitnessValue());
            carsWithWeightedFitness.Add(car);
        }   
        return carsWithWeightedFitness.OrderByDescending(car => car.getFitnessValue()).ToList();
    }

    private List<Car> sortingCarsByPunishingFeatures(List<Car> cars, float penaltyForDirectionChange, float penaltyForDrivingSideways) {
        List<Car> carsWithWeightedFitness = new List<Car>();
        foreach (var car in cars)
        {
            float newFitnessValue = 0;
            for (int genePart = 0; genePart < car.getFitnessValue() - 1; genePart++) {
                if (car.getGeneString()[genePart] == car.getGeneString()[genePart+1]) {
                    //Debug.Log("Those two directions should be the same: " + car.getGeneString()[genePart] + " & " + car.getGeneString()[genePart+1]);
                    newFitnessValue++;
                } else {
                    newFitnessValue = newFitnessValue + 1 * (1 - penaltyForDirectionChange);
                }
                if (car.getGeneString()[genePart] == Car.DirectionsEnum.FORWARD) {
                    newFitnessValue++;
                } else {
                    newFitnessValue = newFitnessValue + 1 * (1 - penaltyForDrivingSideways);
                }
            }
            Debug.Log("Instead of getting " + car.getFitnessValue() + " its new value is " + newFitnessValue);
            car.setFitnessValue(newFitnessValue);
            Debug.Log("New value is " + car.getFitnessValue());
            carsWithWeightedFitness.Add(car);
        }   
        return carsWithWeightedFitness.OrderByDescending(car => car.getFitnessValue()).ToList();
    }

    private List<Car> sortingCarsByPunishingSidewayChoices(List<Car> cars, float penalty) {
        List<Car> carsWithWeightedFitness = new List<Car>();
        foreach (var car in cars)
        {
            float newFitnessValue = 0;
            for (int genePart = 0; genePart < car.getFitnessValue(); genePart++) {
                Debug.Log("I DO NOT nOW SHIIT." + car.getGeneString()[genePart]);

                switch (car.getGeneString()[genePart])
                {
                    case Car.DirectionsEnum.FORWARD:
                        newFitnessValue++;
                        break;

                    case Car.DirectionsEnum.LEFT:
                        newFitnessValue = newFitnessValue + 1 * penalty;
                        break;

                    case Car.DirectionsEnum.RIGHT:
                        newFitnessValue = newFitnessValue + 1 * penalty;
                        break;
                }
            }
            Debug.Log("Instead of getting " + car.getFitnessValue() + " its new value is " + newFitnessValue);
            car.setFitnessValue(newFitnessValue);
            Debug.Log("New value is " + car.getFitnessValue());
            carsWithWeightedFitness.Add(car);
        }   
        return carsWithWeightedFitness.OrderByDescending(car => car.getFitnessValue()).ToList();
    }


    public Car selectParent(List<Car> potentialParents, bool weightedChoice) {
        if (weightedChoice) {
            Debug.Log("Performance oriented world coming up soon ...");
            List<Car> sortedCarsAfterFitnessAscending = potentialParents.OrderBy(car => car.getFitnessValue()).ToList();

            int numberOfIndiviuals = potentialParents.Count();
            List<Car> weightedListOfPotentialParents = new List<Car>();
            for (int i = 0; i < numberOfIndiviuals; i++) {
                int listPosition = i;
                for (int j = 0; j <= listPosition; j++) {
                    weightedListOfPotentialParents.Add(sortedCarsAfterFitnessAscending[i]);
                }
            }

            Debug.Log("There is now the chance " + numberOfIndiviuals + " times higher to choose fittest car.");
            int selectedParentPerformanceBased = Random.Range(0, weightedListOfPotentialParents.Count());
            return weightedListOfPotentialParents[selectedParentPerformanceBased];
        } 
        
        Debug.Log("Same chances for everyone - well the chosen percentage anyways.");
        int selectedParent = Random.Range(0, potentialParents.Count());
        return potentialParents[selectedParent];
    }


    public List<Car.DirectionsEnum> makeChild(Car parent1, Car parent2, float mutationRate, bool crossOver, float crossOverAt)
    {
        List<Car.DirectionsEnum> momsGene = parent1.geneOfIndividual;
        List<Car.DirectionsEnum> dadsGene = parent2.geneOfIndividual;
        List<Car.DirectionsEnum> childGene = new List<Car.DirectionsEnum>();
        Debug.Log("MATE SOMETHING!!");

        if (crossOver) {
            for (int i = 0; i < momsGene.Count() * crossOverAt; i++) {
                childGene.Add(momsGene[i]);
            }
            for (int j = childGene.Count(); j < dadsGene.Count(); j++) {
                childGene.Add(dadsGene[j]);
            }
        } 
        if (!crossOver) {
            List<Car.DirectionsEnum> longerParent = new List<Car.DirectionsEnum>();
            int commonGeneLength = 0;

            if (momsGene.Count() > dadsGene.Count()) {
                longerParent = momsGene;
                commonGeneLength = dadsGene.Count();
            } else {
                longerParent = dadsGene;
                commonGeneLength = momsGene.Count();
            }
            
            for (int geneNr = 0; geneNr < commonGeneLength; geneNr++)
            {
                int selectedParent = Random.Range(0, 2);
                switch (selectedParent)
                {
                    case 0:
                        //Debug.Log("My favourite parent is Mom!");
                        childGene.Add(momsGene[geneNr]);
                        break;
                    case 1:
                        //Debug.Log("My favourite parent is Dad!");
                        childGene.Add(dadsGene[geneNr]);
                        break;
                }
            }

            for (int i = commonGeneLength; i < longerParent.Count(); i++) {
                childGene.Add(longerParent[i]);
            }

            //Debug.Log("Child has gene length of " + childGene.Count() + " and should have length of " + longerParent.Count());
        }

        childGene = MutateGene(childGene, mutationRate);
        return childGene;
    }


    public List<Car.DirectionsEnum> MutateGene(List<Car.DirectionsEnum> gene, float mutationRate)
    {
        List<Car.DirectionsEnum> mutatedGene = new List<Car.DirectionsEnum>();
        for (int i = 0; i < gene.Count(); i++)
        {
            System.Random random = new System.Random();
            if (random.NextDouble() < mutationRate)
            {
                //Debug.Log("Mutation takes effect at " + i + " in gene.");
                mutatedGene.Add((Car.DirectionsEnum)Random.Range(0, 3));
            } else
            {
                mutatedGene.Add(gene[i]);
            }
        }
        return mutatedGene;
    }
}
