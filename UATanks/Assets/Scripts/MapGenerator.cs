﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public int rows;
    public int cols;
    public GameObject[] gridPrefabs;
    public bool mapRandom;
    public int mapSeed;
    public bool mapOTDay;
    

    private float roomWidth = 50.0f;
    private float roomHeight = 50.0f;
    private Room[,] grid;

    // Start is called before the first frame update
    void Start()
    {
        if (mapOTDay)
        {
            mapSeed = DateToInt(DateTime.Now.Date);
        }
        else if (mapRandom)
        {
            mapSeed = DateToInt(DateTime.Now);
        }
        GenerateGrid();
    }

    public int DateToInt(DateTime dateToUse)
    {
        // Add our date up and return it
        return dateToUse.Year + dateToUse.Month + dateToUse.Day + dateToUse.Hour + dateToUse.Minute + dateToUse.Second + dateToUse.Millisecond;
    }

    public GameObject RandomRoomPrefab()
    {
        return gridPrefabs[UnityEngine.Random.Range(0, gridPrefabs.Length)];
    }

    public void GenerateGrid()
    {
        // hold the seed
        UnityEngine.Random.seed = mapSeed;

        // new 2D grid
        grid = new Room[cols, rows];

        // For each grid row...
        for (int i = 0; i < rows; i++)
        {
            // for each column in that row
            for (int j = 0; j < cols; j++)
            {
                // Figure out the location. 
                float xPosition = roomWidth * j;
                float zPosition = roomHeight * i;
                Vector3 newPosition = new Vector3(xPosition, 0.0f, zPosition);

                // Create a new grid at the appropriate location
                GameObject tempRoomObj = Instantiate(RandomRoomPrefab(), newPosition, Quaternion.identity) as GameObject;

                // Set its parent
                tempRoomObj.transform.parent = this.transform;

                // Give it a meaningful name
                tempRoomObj.name = "Room_" + j + "," + i;

                // Get the room object
                Room tempRoom = tempRoomObj.GetComponent<Room>();

                // Open the doors
                // If we are on the bottom row, open the north door
                if (i == 0)
                {
                    tempRoom.doorNorth.SetActive(false);
                }
                else if (i == rows - 1)
                {
                    // Otherwise, if we are on the top row, open the south door
                    tempRoom.doorSouth.SetActive(false);
                }
                else
                {
                    // Otherwise, we are in the middle, so open both doors
                    tempRoom.doorNorth.SetActive(false);
                    tempRoom.doorSouth.SetActive(false);
                }
                // If we are on the first column, open the east door
                if (j == 0)
                {
                    tempRoom.doorEast.SetActive(false);
                }
                else if (j == cols - 1)
                {
                    // Otherwise, if we are on the last column row, open the west door
                    tempRoom.doorWest.SetActive(false);
                }
                else
                {
                    // Otherwise, we are in the middle, so open both doors
                    tempRoom.doorEast.SetActive(false);
                    tempRoom.doorWest.SetActive(false);
                }

                // Save it to the grid array
                grid[j, i] = tempRoom;
            }
        }
    }
}
