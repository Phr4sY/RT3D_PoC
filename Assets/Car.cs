using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    enum DirectionsEnum
    {
        LEFT,
        RIGHT,
        FORWARD
    }

    List<DirectionsEnum> direction = new List<DirectionsEnum>();


    public bool crashed;
    public int distance;
    bool targetReached;
    
    float speed = 200f;
    int deltaTime = 1;
    float nextTime;
    Vector3 movement = new Vector3(0, 0, 0);

    GameObject goal;



    // Start is called before the first frame update
    void Start()
    {
        // Create the initial directions of the car 
        for (int i = 0; i < 2; i++)
        {
            direction.Add( (DirectionsEnum)Random.Range(0, 3));
            Debug.Log(direction[i]);
        }

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
        if (col.gameObject == goal)
        {
            Debug.Log("Reached Goal!");
            //Destroy(goal);
            targetReached = true;
        } else
        {
            // Remove the car from the canvas if it crashes 
            Destroy(this);
            Debug.Log("Collided and killed itself.");
            crashed = true;
        }
    }


    DirectionsEnum getDirection() {
        if (direction.Count>= distance) {
            direction.Add( (DirectionsEnum)Random.Range(0, 3));
            Debug.Log(direction[distance]);
            return direction[distance];
        } 
        Debug.Log("Old DNA: " + direction[distance] + " at step " + distance);
        return direction[distance];      
    }

    // Read the next direction in the array 
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
}