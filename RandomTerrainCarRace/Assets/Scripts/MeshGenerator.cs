using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshGenerator : MonoBehaviour
{
    Mesh mesh;

    public int xSize = 100;
    public int zSize = 650;

    public float offsetMidMax = 99999;
    public float zoomMidMax = 2f;
    public float strengthMidMax = 1.6f;
    public float offsetLargeMax = 99999;
    public float zoomLargeMax = 0.05f;
    public float strengthLargeMax = 14f;

    private float offsetXMid = 0;
    private float offsetYMid = 0;
    private float zoomXMid = 0f;
    private float zoomYMid = 0f;
    private float strengthMid = 0f;

    private float offsetXLarge = 0;
    private float offsetYLarge = 0;
    private float zoomXLarge = 0f;
    private float zoomYLarge = 0f;
    private float strengthLarge = 0f;

    public Material[] materials;
    Vector3[] vertices;
    int[] triangles;

    void Start()
    {   
        // creating random values which will be used in random generated mesh

        // material index of terrain 
        int materialIndex = Random.Range(0, materials.Length);
        // offsetting the perlin noise
        offsetXMid = Random.Range(.5f, offsetMidMax);
        offsetYMid = Random.Range(.5f, offsetMidMax);

        // zoom in for details
        zoomXMid = Random.Range(.4f, zoomMidMax);
        zoomYMid = Random.Range(.4f, zoomMidMax);
        strengthMid = Random.Range(.4f, strengthMidMax);

        offsetXLarge = Random.Range(0f, offsetLargeMax);
        offsetYLarge = Random.Range(0f, offsetLargeMax);
        zoomXLarge = Random.Range(.05f, zoomLargeMax);
        zoomYLarge = Random.Range(.05f, zoomLargeMax);
        strengthLarge = Random.Range(7f, strengthLargeMax);

        // creating the mesh
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        CreateShape();
        UpdateMesh();
        gameObject.AddComponent<MeshCollider>();
        GetComponent<Renderer>().material = materials[materialIndex];
    }

    // deciding meshs verticies
    void CreateShape()
    {
        int xVerticesCount = xSize + 1;
        int zVerticesCount = zSize + 1;

        // decide each vertex position
        vertices = new Vector3[(xVerticesCount) * (zVerticesCount)];
        for (int z = 0; z < zVerticesCount; z++)
        {
            for (int x = 0; x < xVerticesCount; x++)
            {
                float y = 0f;
                // this is for mid (roughness) of terrain (larger zoom for details)
                y += Mathf.PerlinNoise(x * zoomXMid + offsetXMid, z * zoomYMid + offsetYMid) * strengthMid;
                // this is for large (shape) of terrain (smaller zoom for overall shape)
                y += Mathf.PerlinNoise(x * zoomXLarge + offsetXLarge, z * zoomYLarge + offsetYLarge) * strengthLarge;
                // initilize the vertex
                vertices[z * xVerticesCount + x] = new Vector3(x, y, z);
            }
        }

        // this is for connecting the verticies with triangles 
        // deciding the each triangles's verticies 
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
        // we say the mesh we want to unity by passing the decided verticies and traingles to created mesh
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
    }
}
