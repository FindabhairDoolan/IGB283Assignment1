using UnityEngine;

public class Triangle : MonoBehaviour
{
    [SerializeField] private Material material;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Add a mesh filter and mesh renderer, getting and setting useful variables
        Mesh mesh = gameObject.AddComponent<MeshFilter>().mesh;
        gameObject.AddComponent<MeshRenderer>().material = material;

        // Clear all vertex and index data from the mesh
        mesh.Clear();

        // Create a triangle between(0, 0, 0), (0, 1, 0), and(1, 1, 0)
        mesh.vertices = new Vector3[]
        {
            new Vector3(0, 0, 0),
            new Vector3(0, 1, 0),
            new Vector3(1, 1, 0)
        };
        // Set the triangle colours
        Color triColour = new Color(0.8f, 0.3f, 0.3f, 1f); // Pale red
        mesh.colors = new Color[]
        {
            triColour,
            triColour,
            triColour
        };

        // Specify which vertices the triangle uses
        mesh.triangles = new int[] { 0, 1, 2 };
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
