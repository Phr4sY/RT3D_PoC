using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class RoadGenerator : MonoBehaviour
{
    public GameObject wallPrefab;
    public GameObject target;

    public int laneWidth = 3; 
    public int mapLength = 20; 
    public float wallWidth = 1;
    public int curveFrequency = 1; 


    float xOffset = 0f;

    private System.Random random;

    private void Start()
    {
        GenerateMap();
    }

    private void GenerateMap()
    {
        SpawnWalls(new Vector3(0, 0.5f * wallWidth, 0), Quaternion.identity);

        Vector3 currentPos = new Vector3(0, 0.5f * wallWidth,0);
        float currentOffset = 0f;

        for (int i = 0; i <= mapLength; i++)
        {
            if (i == mapLength)
            {
                currentPos += new Vector3(0, wallPrefab.transform.localScale.y, 0);

                GameObject instantiatedObject = Instantiate(target, new Vector3(currentOffset, currentPos.y, 0), Quaternion.identity);
                instantiatedObject.transform.localScale = new Vector3(laneWidth * 2, 1, 1);
            }
            else
            {
                currentPos += new Vector3(0, wallPrefab.transform.localScale.y, 0);


                xOffset = Random.Range(-10f, 10f) * laneWidth;


                // Update the current offset gradually
                currentOffset += Mathf.Clamp(xOffset, -1, 1);

                SpawnWalls(currentPos + new Vector3(currentOffset, 0, 0), Quaternion.identity);
            }
        }
    }



    private void SpawnWalls(Vector3 position, Quaternion rotation)
    {
        // Spawn left wall
        GameObject leftWall = Instantiate(wallPrefab, position + new Vector3(-laneWidth, 0, 0), rotation);
        leftWall.transform.localScale = new Vector3(wallWidth, wallWidth, wallWidth); 

        // Spawn right wall
        GameObject rightWall = Instantiate(wallPrefab, position + new Vector3(laneWidth, 0, 0), rotation);
        rightWall.transform.localScale = new Vector3(wallWidth, wallWidth, wallWidth); // Ändere die Skalierung der rechten Wand
    }


}

