using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class ProceduralMesh : MonoBehaviour
{
    public Mesh mesh;
    public List<Vector3> vertices;
    public List<int> triangles;

    public ContactPoint contactPoint;

    [SerializeField]
    public Vector3 contact { get { return contactPoint.point; } }

    private void Awake()
    {
        mesh = GetComponent<MeshFilter>().mesh;

        Vector3[] tempVertices = mesh.vertices;
        int[] tempTris = mesh.triangles;

        for(int i = 0; i < tempVertices.Length; i++)
        {
            vertices.Add(tempVertices[i]);
        }

        for (int i = 0; i < tempTris.Length; i++)
        {
            triangles.Add(tempTris[i]);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //MakeMeshData();
        //CreateMesh();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void MakeMeshData()
    {
        //vertices = new Vector3[] { new Vector3(0, 0, 0), new Vector3(0, 0, 1), new Vector3(1, 0, 0), new Vector3(1, 0, 1)};

        //triangles = new int[] { 0, 1, 2, 2, 1 , 3 };

    }

    void CreateMesh()
    {
        mesh.Clear();
        mesh.vertices = vertices.ToArray();
        mesh.triangles = triangles.ToArray(); ;


        mesh.RecalculateNormals();
    }

    void TryToUpdateMesh()
    {
        mesh.Clear();
        mesh.vertices = vertices.ToArray();
        bull();
        mesh.triangles = triangles.ToArray(); ;
        this.gameObject.GetComponent<MeshFilter>().mesh = mesh;
    }

    void bull()
    {

    }

    public void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Knife"))
        {
            contactPoint = collision.GetContact(0);
            Debug.Log($"Knife Hit Object at {contact.ToString()}");
            
            SearchThing();

            Debug.Break();

        }

    }

    private void SearchThing()
    {
        bool matched = false;
        for (int v = 0; v < vertices.Count; v++)
        {
            if(Mathf.Abs( vertices[v].normalized.y - contact.y) < 0.1)//(((vertices[v].y / vertices[v].magnitude) == (contact.y / contact.magnitude))) // && (vertices[v].z / 100 == contact.z || vertices[v].x / 100 == contact.x))
            {
                Debug.Log("Y approx Match");
                if (contactPoint.point.z == vertices[v].z)
                {
                    // z match
                }
                else
                {
                    // x match
                    vertices.Insert((v+1), contactPoint.point);
                    v += 1;
                }
            }
        }

        if (!matched)
            Debug.Log("No matches");
        else
            TryToUpdateMesh();
    }
}
