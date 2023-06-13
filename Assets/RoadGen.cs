using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadGen : MonoBehaviour
{
    public GameObject wallPrefab;
    public float roadLength = 20f;
    public float roadWidth = 10f;
    public float wallHeight = 1f;

    private void Start()
    {
        GenerateRoad();
    }

    private void GenerateRoad()
    {
        // Spawn left and right walls at the starting position
        SpawnWall(Vector3.zero);
        SpawnWall(Vector3.zero + new Vector3(roadWidth, 0f, 0f));

        // Generate the road in negative x-direction
        for (float x = 0f; x > -roadLength; x -= roadWidth)
        {
            Vector3 position = new Vector3(x, 0f, 0f);
            SpawnWall(position);
        }

        // Generate the road in positive y-direction
        for (float y = 0f; y < roadLength; y += roadWidth)
        {
            Vector3 position = new Vector3(-roadLength, y, 0f);
            SpawnWall(position);
        }

        // Generate the road in positive x-direction
        for (float x = -roadLength; x < 0f; x += roadWidth)
        {
            Vector3 position = new Vector3(x, roadLength, 0f);
            SpawnWall(position);
        }
    }

    private void SpawnWall(Vector3 position)
    {
        GameObject wall = Instantiate(wallPrefab, position, Quaternion.identity);
        wall.transform.localScale = new Vector3(roadWidth, wallHeight, 1f);
    }
}
