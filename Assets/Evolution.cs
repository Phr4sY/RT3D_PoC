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
        if (numberOfFittestIndividuals < 1) {
            Debug.Log("Survival rate is too small. Survival rate is set to 1.");
            numberOfFittestIndividuals = 1;
        }
        Debug.Log("Number of fittest individuals is " + numberOfFittestIndividuals);
        List<Car> sortedCarsAfterFitness = cars.OrderBy(car => car.getFitnessValue()).ToList();
        List<Car> fittestSurviverCars = sortedCarsAfterFitness.Take(numberOfFittestIndividuals).ToList();
        return fittestSurviverCars;
    }


    public List<Car.DirectionsEnum> makeChild(Car parent1, Car parent2, float mutationRate, bool crossOver)
    {
        List<Car.DirectionsEnum> momsGene = parent1.geneOfIndividual;
        List<Car.DirectionsEnum> dadsGene = parent2.geneOfIndividual;
        List<Car.DirectionsEnum> childGene = new List<Car.DirectionsEnum>();
        Debug.Log("MATE SOMETHING!!");

         if (crossOver) {
            for (int i = 0; i < momsGene.Count() / 2; i++) {
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
