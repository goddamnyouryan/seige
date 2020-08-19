using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [Range(0, 100)]
    public int randomFillPercent;
    public int width, height, explosionRadius;
    public string seed;
    public bool useRandomSeed;

    int[,] map;

    void Start() {
        GenerateMap();
        BoxCollider box = gameObject.AddComponent<BoxCollider>();
        box.size = new Vector3(width, height);
    }

    void Update() {
        if (Input.GetMouseButtonDown(0)) {
            RaycastHit hitInfo;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo)) {
                UpdateMap(hitInfo.point);
            }
        }
    }

    void GenerateMap() {
        map = new int[width, height];
        //RandomFillMap();
        FillMap();

        /*
        for (int i = 0; i < 5; i++) {
            SmoothMap();
        }
        */

        GenerateMesh();
    }

    void GenerateMesh() {
        MeshGenerator meshGen = GetComponent<MeshGenerator>();
        meshGen.GenerateMesh(map, 1);
    }

    void FillMap() {
        for (int x = 0; x < width; x++) {
            for (int y = 0; y < height; y++) {
                map[x,y] = 1;
            }
        }
    }

    void UpdateMap(Vector3 point) {
        int centerX = (int) (point.x + width / 2f);
        int centerY = (int) (point.y + height / 2f);

        int xStart = centerX - explosionRadius;
        if (xStart < 0)
            xStart = 0;
        int xEnd = centerX + explosionRadius;
        if (xEnd >= width)
            xEnd = width;
        int yStart = centerY - explosionRadius;
        if (yStart < 0)
            yStart = 0;
        int yEnd = centerY + explosionRadius;
        if (yEnd >= height)
            yEnd = height;

        for (int x = xStart; x < xEnd; x++) {
            for (int y = yStart; y < yEnd; y++) {
                map[x,y] = 0;
            }
        }

        // maybe just do one of those (int x = x - radius; x < x + radius;) type for loops and check the square first?
        //for (int x = 0; x < width; x++) {
        //    for (int y = 0; y < height; y++) {
        //        float diffX = Mathf.Abs(x - point.x);
        //        float diffY = Mathf.Abs(y - point.y);
        //        float radSquared = (float) explosionRadius * explosionRadius;
//
        //        float pythag = (float) Math.Sqrt(diffX * diffX + diffY * diffY);
//
        //        Console.Log(pythag, radSquared, pythag < radSquared);
//
        //        if (pythag < radSquared) {
        //            map[x,y] = 0;
        //        } else {
        //            map[x,y] = 1;
        //        }
        //    }
        //}

        GenerateMesh();
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
