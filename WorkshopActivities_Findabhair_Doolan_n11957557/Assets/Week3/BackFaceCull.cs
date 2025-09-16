using System.Collections.Generic;
using UnityEngine;

public class BackFaceCull : MonoBehaviour
{
    private Mesh mesh;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Get the Mesh from the Mesh Filter
        mesh = GetComponent<MeshFilter>().mesh;

        // Cull the back faces
        CullBackFaces();
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Cull back faces
    private void CullBackFaces()
    {
        // Create a list for the triangles to keep
        List<int> triangles = new List<int>();

        for (int i = 0; i < mesh.triangles.Length; i = i + 3)
        {
            // Get the vertices of the current triangle
            Vector3 v0 = mesh.vertices[mesh.triangles[i + 0]];
            Vector3 v1 = mesh.vertices[mesh.triangles[i + 1]];
            Vector3 v2 = mesh.vertices[mesh.triangles[i + 2]];

            // Create displacement vectors representing the triangle from point v0
            Vector3 s0 = v1 - v0;
            Vector3 s1 = v2 - v0;

            // Find the triangle's normal
            Vector3 normal = Vector3.Cross(s1, s0);

            // Find the dot product with the view direction
            Vector3 viewDirection = Camera.main.transform.forward;
            float dotProduct = Vector3.Dot(viewDirection, normal);

            // If the face is visible, add the indices to the new list
            if (dotProduct > 0f)
            {
                triangles.Add(mesh.triangles[i + 0]);
                triangles.Add(mesh.triangles[i + 1]);
                triangles.Add(mesh.triangles[i + 2]);
            }
        }
        // Replace the mesh's triangles
        mesh.triangles = triangles.ToArray();
    }
}
