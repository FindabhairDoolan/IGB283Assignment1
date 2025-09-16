using UnityEngine;
using System.Collections.Generic;

public class BackFaceCulllReal : MonoBehaviour
{
    private Mesh mesh;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mesh = GetComponent<MeshFilter>().mesh;

        CullBackFaces();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void CullBackFaces()
    {
        List<int> triangles = new List<int>();

        for (int i = 0; i < mesh.triangles.Length; i = i + 3)
        {
            Vector3 v0 = mesh.vertices[mesh.triangles[i + 0]];
            Vector3 v1 = mesh.vertices[mesh.triangles[i + 1]];
            Vector3 v2 = mesh.vertices[mesh.triangles[i + 2]];

            Vector3 s0 = v1 - v0;
            Vector3 s1 = v2 - v0;

            Vector3 normal = Vector3.Cross(s1, s0);

            Vector3 viewDirection = Camera.main.transform.forward;
            float dotProduct = Vector3.Dot(viewDirection, normal);

            if (dotProduct > 0f)
            {
                triangles.Add(mesh.triangles[i + 0]);
                triangles.Add(mesh.triangles[i + 1]);
                triangles.Add(mesh.triangles[i + 2]);
            }
        }
        mesh.triangles = triangles.ToArray();
    }
}
