using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadGenerator : MonoBehaviour
{
    public GameObject wallPrefab;

    public int laneWidth = 3; // Width of the lane
    public int mapLength = 20; // Length of the map
    public int curveFrequency = 1; // Frequency of curves


    float xOffset = 0f;

    private System.Random random;

    private void Start()
    {
        GenerateMap();
    }

    private void GenerateMap()
    {
        SpawnWalls(Vector3.zero, Quaternion.identity);

        Vector3 currentPos = Vector3.zero;
        float currentOffset = 0f;

        for (int i = 0; i < mapLength; i++)
        {

            currentPos += new Vector3(0, wallPrefab.transform.localScale.y, 0);


            xOffset = Random.Range(-10f, 10f) * laneWidth;


            // Update the current offset gradually
            currentOffset += Mathf.Clamp(xOffset, -1, 1);

            SpawnWalls(currentPos + new Vector3(currentOffset, 0, 0), Quaternion.identity);
        }
    }



    private void SpawnWalls(Vector3 position, Quaternion rotation)
    {
        // Spawn left wall
        Instantiate(wallPrefab, position + new Vector3(-laneWidth, 0, 0), rotation);

        // Spawn right wall
        Instantiate(wallPrefab, position + new Vector3(laneWidth, 0, 0), rotation);
    }


}

