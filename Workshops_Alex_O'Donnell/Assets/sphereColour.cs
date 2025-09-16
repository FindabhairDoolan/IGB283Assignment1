using UnityEngine;

public class sphereColour : MonoBehaviour
{
    [SerializeField] private Color colour1 = Color.magenta;
    [SerializeField] private Color colour2 = Color.red;
    private Mesh mesh;

    void Start()
    {
        mesh = GetComponent<MeshFilter>().mesh;
    }

    
    void Update()
    {
        SetMeshColour();
    }

    private void SetMeshColour()
    {
        Vector3 viewDirection = -Camera.main.transform.forward;

        Vector3[] vertices = mesh.vertices;
        Vector3[] normals = mesh.normals;
        Color[] colours = new Color[normals.Length];

        for (int i = 0; i < normals.Length; i++)
        {
            Vector3 rotatedNormal = transform.rotation * normals[i];
            float dotProduct = Vector3.Dot(viewDirection, rotatedNormal);
            colours[i] = Color.Lerp(colour2, colour1, dotProduct);
            mesh.colors = colours;

        }
    }
}
