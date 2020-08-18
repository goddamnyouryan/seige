﻿using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [Range(0, 100)]
    public int randomFillPercent;
    public int width, height;
    public string seed;
    public bool useRandomSeed;

    int[,] map;

    void Start() {
        GenerateMap();
    }

    void Update() {
        if (Input.GetMouseButtonDown(0)) {
            GenerateMap();
        }
    }

    void GenerateMap() {
        map = new int[width, height];
        RandomFillMap();

        for (int i = 0; i < 5; i++) {
            SmoothMap();
        }

        MeshGenerator meshGen = GetComponent<MeshGenerator>();
        meshGen.GenerateMesh(map, 1);
    }

    void RandomFillMap() {
        if (useRandomSeed) {
            seed = Time.time.ToString();
        }

        System.Random random = new System.Random(seed.GetHashCode());

        for (int x = 0; x < width; x++) {
            for (int y = 0; y < height; y++) {
                // Make the edges always walls
                if (x == 0 || x == width - 1 || y == 0 || y == height -1) {
                    map[x,y] = 1;
                } else {
                    map[x,y] = (random.Next(0, 100) < randomFillPercent) ? 1 : 0;
                }
            }
        }
    }

    void SmoothMap() {
        for (int x = 0; x < width; x++) {
            for (int y = 0; y < height; y++) {
                int neighborWallCount = GetSurroundingWallCount(x, y);

                if (neighborWallCount > 4) {
                    map[x,y] = 1;
                } else if (neighborWallCount < 4) {
                    map[x,y] = 0;
                }
            }
        }
    }

    int GetSurroundingWallCount(int gridX, int gridY) {
        int wallCount = 0;

        for (int neighborX = gridX - 1; neighborX <= gridX + 1; neighborX++) {
            for (int neighborY = gridY - 1; neighborY <= gridY + 1; neighborY++) {
                if (neighborX >= 0 && neighborX < width && neighborY >= 0 && neighborY < height) {
                    // ignore the original tile
                    if (neighborX != gridX || neighborY != gridY) {
                        wallCount += map[neighborX, neighborY];
                    }
                } else {
                    wallCount++;
                }
            }
        }

        return wallCount;
    }

    void OnDrawGizmos () {
        /*
        if (map != null) {
            for (int x = 0; x < width; x++) {
                for (int y = 0; y < height; y++) {
                    Gizmos.color = (map[x,y] == 1) ? Color.black : Color.white;
                    Vector3 position = new Vector3(-width / 2 + x + 0.5f, 0, -height/2 + y + 0.5f);
                    Gizmos.DrawCube(position, Vector3.one);
                }
            }
        }
        */
    }
}
