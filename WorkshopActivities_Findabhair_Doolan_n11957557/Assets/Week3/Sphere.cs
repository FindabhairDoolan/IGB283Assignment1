using UnityEngine;

public class Sphere : MonoBehaviour
{
    [SerializeField] private Color colour1 = Color.white;
    [SerializeField] private Color colour2 = Color.white;
    private Mesh mesh;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Get the Mesh from the Mesh Filter
        mesh = GetComponent<MeshFilter>().mesh;
    }

    // Update is called once per frame
    void Update()
    {
        SetMeshColour();
    }

    // Set the colour of the mesh from the normal
    private void SetMeshColour()
    {
        // Get the negative view direction
        Vector3 viewDirection = -Camera.main.transform.forward;

        // Initialise the mesh data arrays
        Vector3[] vertices = mesh.vertices;
        Vector3[] normals = mesh.normals;
        Color[] colours = new Color[normals.Length];

        // Colour each vertex with the dot product between the view and normal
        for (int i = 0; i < normals.Length; i++)
        {
            // Account for the sphere rotation
            Vector3 rotatedNormal = transform.rotation * normals[i];
            float dotProduct = Vector3.Dot(viewDirection, rotatedNormal);

            // Interpolate between colour1 and colour2 based on the dot product
            colours[i] = Color.Lerp(colour2, colour1, dotProduct);
        }

        // Assign the new colours
        mesh.colors = colours;
    }
}
