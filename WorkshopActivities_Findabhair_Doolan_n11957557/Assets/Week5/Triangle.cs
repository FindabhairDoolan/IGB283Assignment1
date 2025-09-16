using UnityEngine;

public class Triangle : MonoBehaviour
{
    [SerializeField] private Material material;
    private Mesh mesh;
    [SerializeField] private float angle = 10f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Add a mesh filter and mesh renderer, getting and setting useful variables
        mesh = gameObject.AddComponent<MeshFilter>().mesh;
        gameObject.AddComponent<MeshRenderer>().material = material;
        // Clear all vertex and index data from the mesh
        mesh.Clear();
        // Create a triangle between (0,0,0), (0,1,0), and (1,1,0)
        mesh.vertices = new Vector3[]
        {
            new Vector3(0, 0, 0),
            new Vector3(0, 1, 0),
            new Vector3(1, 1, 0),
            new Vector3(1, 0, 0),
            new Vector3(1, -1, 0)
        };
        // Set the triangle colours
        Color triColour = new Color(0.8f, 0.3f, 0.3f, 1f); // Pale red
        mesh.colors = new Color[]
        {
            triColour,
            triColour,
            triColour,
            triColour,
            triColour
        };
        // Specify which vertices the triangle uses
        mesh.triangles = new int[] 
        {
            0, 1, 2,
            0, 2, 3,
            0, 3, 4
        };
    }

    // Update is called once per frame
    void Update()
    {
        RotateTriangle();
    }

    private Matrix3x3 Rotate(float angle)
    {
        // Calculate sin and cos of the angle once
        float sin = Mathf.Sin(angle);
        float cos = Mathf.Cos(angle);

        // Create a new matrix for rotation
        Matrix3x3 r = new Matrix3x3(
        new Vector3(cos, -sin, 0),
        new Vector3(sin, cos, 0),
        new Vector3(0, 0, 1));
        return r;
    }

    private void RotateTriangle()
    {
        // Get the current mesh vertices
        Vector3[] vertices = mesh.vertices;
        // Get a rotation matrix for the given angular speed
        Matrix3x3 r = Rotate(angle * Time.deltaTime);

        // Rotate every vertex in the mesh
        for (int i = 0; i < vertices.Length; i++)
        {
            vertices[i] = r.MultiplyPoint(vertices[i]);
        }

        // Update the mesh vertices
        mesh.vertices = vertices;
        // Recalculate the mesh bounds
        mesh.RecalculateBounds();


    }
}
