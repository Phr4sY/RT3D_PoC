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
    public bool targetReached;
    
    float speed = 0.9f;
    public float deltaTime = 1.0f;
    float nextTime;
    Vector3 movement = new Vector3(0, 0, 0);

    Collision2D col;
    string goal = "Triangle";


    // Start is called before the first frame update
    void Start()
    {
        // set the starting distance to 0, target reached and crashed to false 
        distance = 0;
        targetReached = false;
        crashed = false;

        // Initial position
        movement = new Vector3(0, 0, 0);
        transform.Translate(movement, Space.World);
        nextTime = Time.time + deltaTime;
        Debug.Log("GEENEE" + geneOfIndividual.Count);
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= nextTime)
        {
            setDirection(getDirection(distance));
            distance++;
            nextTime = Time.time + deltaTime;
        }
        
    }

    
    void OnCollisionEnter2D(Collision2D col)
    {

        // Remove the car from the canvas if it finishes the course
        if (col.transform.gameObject.name == goal)
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
        Debug.Log("Old DNA has " + geneOfIndividual.Count + " genes at step " + distance);
       
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
                movement = Vector3.up * speed * deltaTime;
                break;

            case DirectionsEnum.LEFT:
                //drive left
                movement = Vector3.left * speed * deltaTime;
                break;

            case DirectionsEnum.RIGHT:
                //drive right
                movement = Vector3.right * speed * deltaTime;
                break;
        }
        transform.Translate(movement, Space.World);
    }

    public int getFitnessValue(){
        return distance;
    }

    public void setInheritedGenes(List<DirectionsEnum> givenGenes) {
        geneOfIndividual.AddRange(givenGenes);
    }

    public List<DirectionsEnum> getGeneString() {
        return geneOfIndividual;
    }
}