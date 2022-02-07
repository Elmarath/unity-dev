using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateTerrainDemo : MonoBehaviour
{
    Mesh mesh;


    [Header("Init Terrain")]
    public int xSizeMax = 50;
    public int zSizeMax = 50;

    public float offsetMidMax = 99999;
    public float zoomMidMax = .9f;
    public float strengthMidMax = 1.6f;
    public float offsetLargeMax = 99999;
    public float zoomLargeMax = 0.1f;
    public float strengthLargeMax = 10f;

    
    [Header("Terrain Current Variables")]
    public int xSize = 50;
    public int zSize = 50;

    public float timespeed = 2f;

    public float offsetXMid = 0;
    public float offsetYMid = 0;
    public float zoomXMid = 0f;
    public float zoomYMid = 0f;
    public float strengthMid = 0f;

    public float offsetXLarge = 0;
    public float offsetYLarge = 0;
    public float zoomXLarge = 0f;
    public float zoomYLarge = 0f;
    public float strengthLarge = 0f;
    public int materialIndex;

    public Material[] materials;
    Vector3[] vertices;
    int[] triangles;

    void Awake()
    {
        materialIndex = Random.Range(0, materials.Length);
        offsetXMid = Random.Range(0f, offsetMidMax);
        offsetYMid = Random.Range(0f, offsetMidMax);
        zoomXMid = Random.Range(.1f, zoomMidMax);
        zoomYMid = Random.Range(.1f, zoomMidMax);
        strengthMid = Random.Range(.7f, strengthMidMax);

        offsetXLarge = Random.Range(0f, offsetLargeMax);
        offsetYLarge = Random.Range(0f, offsetLargeMax);
        zoomXLarge = Random.Range(.05f, zoomLargeMax);
        zoomYLarge = Random.Range(.05f, zoomLargeMax);
        strengthLarge = Random.Range(6f, strengthLargeMax);
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
    }

    void Update()
    {
        CreateShape();
        UpdateMesh();
        GetComponent<Renderer>().material = materials[materialIndex];
    }

    void CreateShape()
    {
        int xVerticesCount = xSize + 1;
        int zVerticesCount = zSize + 1;

        vertices = new Vector3[(xVerticesCount) * (zVerticesCount)];
        for (int z = 0; z < zVerticesCount; z++)
        {
            for (int x = 0; x < xVerticesCount; x++)
            {
                float y = 0f;
                y += Mathf.PerlinNoise(x * zoomXMid + offsetXMid, z * zoomYMid + offsetYMid) * strengthMid;
                y += Mathf.PerlinNoise(x * zoomXLarge + offsetXLarge, z * zoomYLarge + offsetYLarge) * strengthLarge;
                vertices[z * xVerticesCount + x] = new Vector3(x, y, z);
            }
        }

        triangles = new int[xSize * zSize * 6];
        int vert = 0;
        int tris = 0;
        for (int z = 0; z < zSize; z++)
        {
            for (int x = 0; x < xSize; x++)
            {
                triangles[tris + 0] = vert + 0;
                triangles[tris + 1] = vert + xSize + 1;
                triangles[tris + 2] = vert + 1;
                triangles[tris + 3] = vert + 1;
                triangles[tris + 4] = vert + xSize + 1;
                triangles[tris + 5] = vert + xSize + 2;
                vert++;
                tris += 6;
            }
            vert++;
        }
    }

    void UpdateMesh()
    {
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;

        offsetXLarge += Time.deltaTime * timespeed;
        offsetYLarge += Time.deltaTime * timespeed;
        

        mesh.RecalculateNormals();
    }
}
