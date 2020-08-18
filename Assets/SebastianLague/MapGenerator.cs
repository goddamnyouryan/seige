using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public int width, height;

    [Range(0, 100)]
    public int randomFillPercent;
    int[,] map;

    void Start() {
        GenerateMap();
    }

    void GenerateMap() {
        map = new int[width, height];
    }

}
