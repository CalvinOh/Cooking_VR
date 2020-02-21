using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCSCript : MonoBehaviour
{
    Mesh mesh;
    public Vector3[] itemVertices;
    int[] triangles;
    // Start is called before the first frame update
    void Start()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        itemVertices = this.GetComponent<Mesh>().vertices;

        itemVertices = new Vector3[] {new Vector3(0, 0, 0), new Vector3(0, 0, 1), new Vector3(1, 0, 0), new Vector3(1, 0, 1) };
        triangles = new int[] { 0, 1, 2, 2, 1, 3 };
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
