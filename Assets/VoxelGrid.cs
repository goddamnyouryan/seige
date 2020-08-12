using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
public class VoxelGrid : MonoBehaviour
{
    public int resolution;
    public GameObject voxelPrefab;

    private bool[] voxels;
    private float voxelSize;
    private Material[] voxelMaterials;

    public void Initialize(int resolution, float size) {
        Console.Log("Initialize", resolution, size);
        this.resolution = resolution;
        voxelSize = size / resolution;
        voxels = new bool[resolution * resolution];
        voxelMaterials = new Material[voxels.Length];

        for (int i = 0, y = 0; y < resolution; y++) {
            for (int x = 0; x < resolution; x++, i++) {
                CreateVoxel(i, x, y);
            }
        }

        SetVoxelColors();
    }

    private void CreateVoxel(int i, int x, int y) {
        GameObject o = Instantiate(voxelPrefab) as GameObject;
        o.transform.parent = transform;
        o.transform.localPosition = new Vector3((x + 0.5f) *  voxelSize, (y + 0.5f) * voxelSize);
        o.transform.localScale = Vector3.one  * voxelSize;
        voxelMaterials[i] = o.GetComponent<MeshRenderer>().material;
    }

    public void SetVoxel(int x, int y, bool state) {
        // LogColor(y * resolution + x);
        voxels[y * resolution + x] = state;
        SetVoxelColors();

    }

    private void SetVoxelColors() {
        for (int i = 0; i < voxels.Length; i++) {
            voxelMaterials[i].color = voxels[i] ? Color.red : Color.white;
        }
    }

    private void LogColor(int i) {
        Console.Log(i, voxelMaterials[i].color, voxels[i]);
    }
}
