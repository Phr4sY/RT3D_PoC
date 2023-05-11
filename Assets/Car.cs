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
    public int distance;
    public bool targetReached;
    
    float speed = 200f;
    int deltaTime = 1;
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

    }

    // Update is called once per frame
    void Update()
    {
        if ((Time.time >= nextTime) && (distance < 100))
        {
            setDirection(getDirection());
            distance++;
            nextTime += deltaTime;
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


    DirectionsEnum getDirection() {
        if (geneOfIndividual.Count>= distance) {
            geneOfIndividual.Add((DirectionsEnum)Random.Range(0, 3));
            //Debug.Log(geneOfIndividual[distance]);
            return geneOfIndividual[distance];
        } 
        Debug.Log("Old DNA: " + geneOfIndividual[distance] + " at step " + distance);
        return geneOfIndividual[distance];      
    }

    // Read the next geneOfIndividual in the array 
    void setDirection(DirectionsEnum currentDirection)
    {
        switch (currentDirection)
        {
            case DirectionsEnum.FORWARD:
                //drive forward
                movement = Vector3.up * speed * Time.deltaTime;
                break;

            case DirectionsEnum.LEFT:
                //drive left
                movement = Vector3.left * speed * Time.deltaTime;
                break;

            case DirectionsEnum.RIGHT:
                //drive right
                movement = Vector3.right * speed * Time.deltaTime;
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
}