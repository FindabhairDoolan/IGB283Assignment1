using UnityEngine;

public class ClockHand : MonoBehaviour
{
    [SerializeField] private Material material;
    private Mesh mesh;
    [SerializeField] private float rotationTime = 10f;
    [SerializeField] private float length;
    [SerializeField] private float width;
    [SerializeField] private Color colour;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Add a mesh filter and mesh renderer, getting and setting useful variables
        mesh = gameObject.AddComponent<MeshFilter>().mesh;
        gameObject.AddComponent<MeshRenderer>().material = material;
        // Clear all vertex and index data from the mesh
        mesh.Clear();

        // Create a rectangle
        mesh.vertices = new Vector3[]
        {
            new Vector3(-width/2, 0, 0), //Make in middle (-0.5 over)
            new Vector3(-width/2, length, 0),
            new Vector3(width/2, length, 0),
            new Vector3(width/2, 0, 0)    
        };

        // Set the colours
        mesh.colors = new Color[] 
        { 
            colour,
            colour,
            colour,
            colour
        };
        
        // Specify which vertices the triangle uses
        mesh.triangles = new int[] 
        { 
            0, 1, 2,
            0, 2, 3
        };
    }

    // Update is called once per frame
    void Update()
    {
        float angle = 2.0f * Mathf.PI / rotationTime;
        RotateHand(angle);
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

    private void RotateHand(float angle)
    {
        // Get the current mesh vertices
        Vector3[] vertices = mesh.vertices;
        // Get a rotation matrix for the given angular speed
        Matrix3x3 r = Rotate(-angle * Time.deltaTime);

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
