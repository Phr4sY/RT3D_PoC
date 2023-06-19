using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    public enum DirectionsEnum
    {
        LEFT,
        RIGHT,
        FORWARD
    }

    public List<DirectionsEnum> geneOfIndividual = new List<DirectionsEnum>();

    public bool crashed;
    private int distance;
    private float fitnessValue;
    public bool targetReached;
    
    public float stepSize = 0.9f;
    public float deltaTime = 1.0f;
    float nextTime;
    Vector3 movement = new Vector3(0, 0, 0);

    Collision2D col;


    // Start is called before the first frame update
    void Start()
    {
        // set the starting distance to 0, target reached and crashed to false 
        distance = 0;
        fitnessValue = 0;
        targetReached = false;
        crashed = false;

        // Initial position
        movement = new Vector3(0, 0, 0);
        transform.Translate(movement, Space.World);
        nextTime = Time.time + deltaTime;

        if (stepSize > 1 || stepSize <= 0) {
            Debug.Log("Step size either too large or too small. Choose a value between 0 and 1. Set back to 1 on default.");
            stepSize = 1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= nextTime)
        {
            setDirection(getDirection(distance));
            distance++;
            fitnessValue = distance;
            nextTime = Time.time + deltaTime;
        }
    }

    
    void OnCollisionEnter2D(Collision2D col)
    {

        // Remove the car from the canvas if it finishes the course
        if (col.gameObject.CompareTag("Target"))
        {
            Debug.Log("Reached Goal !!!");
            Destroy(this);
            targetReached = true;
            crashed = true;
        } else
        {
            // Remove the car from the canvas if it crashes 
            Destroy(this);
            Debug.Log("Collided and killed itself.");
            crashed = true;
        }
    }


    DirectionsEnum getDirection(int distance) {
        //Debug.Log("Old DNA has " + geneOfIndividual.Count + " genes at step " + distance);
       
        if (distance >= geneOfIndividual.Count) {
            //Debug.Log("Nr of original gene " + geneOfIndividual.Count + " is hopefully smaller than distance travelled " + distance);
            geneOfIndividual.Add((DirectionsEnum)Random.Range(0, 3));
            return geneOfIndividual[distance];
        } 
        //Debug.Log("Old DNA: " + geneOfIndividual[distance] + " at step " + distance);
        return geneOfIndividual[distance];      
    }


    // Read the next geneOfIndividual in the array 
    void setDirection(DirectionsEnum currentDirection)
    {
        switch (currentDirection)
        {
            case DirectionsEnum.FORWARD:
                //drive forward
                movement = Vector3.up * stepSize;
                break;

            case DirectionsEnum.LEFT:
                //drive left
                movement = Vector3.left * stepSize;
                break;

            case DirectionsEnum.RIGHT:
                //drive right
                movement = Vector3.right * stepSize;
                break;
        }
        transform.Translate(movement, Space.World);
    }

    public float getFitnessValue(){
        return fitnessValue;
    }

    public void setFitnessValue(float newFitnessValue) {
        fitnessValue = newFitnessValue;
    }

    public void setInheritedGenes(List<DirectionsEnum> givenGenes) {
        geneOfIndividual.AddRange(givenGenes);
    }

    public List<DirectionsEnum> getGeneString() {
        return geneOfIndividual;
    }
}