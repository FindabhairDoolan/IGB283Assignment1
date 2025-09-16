using UnityEngine;
public class Triangle : MonoBehaviour
{
    [SerializeField] private Material material;
    [SerializeField] private float angle = 10f;
    private Mesh mesh;
    // Start is called once before the first execution of Update after
    
    void Start()
    {
        
        mesh = gameObject.AddComponent<MeshFilter>().mesh;
        gameObject.AddComponent<MeshRenderer>().material = material;
        
        mesh.Clear();

        mesh.vertices = new Vector3[]
        {
        new Vector3(0, 0, 0),
        new Vector3(0, 1, 0),
        new Vector3(1, 1, 0),
        new Vector3(1, 0, 0),
        new Vector3(1, -1, 0)
        };
        
        Color triColour = new Color(0.8f, 0.3f, 0.3f, 1f); // Pale red
        mesh.colors = new Color[]
        {
        triColour,
        triColour,
        triColour,
        triColour,
        triColour
        };
       
        mesh.triangles = new int[] {
            0, 1, 2,
            0, 2, 3,
            4, 0, 3,};
    }
    // Update is called once per frame
    void Update()
    {
        RotateTriangle();
    }

    private Matrix3x3 Rotate(float angle)
    {
        float sin = Mathf.Sin(angle);
        float cos = Mathf.Cos(angle);

        Matrix3x3 r = new Matrix3x3(
            new Vector3(cos, -sin, 0),
            new Vector3(sin, cos, 0),
            new Vector3(0, 0, 1)
        );
        return r;
    }

    private void RotateTriangle()
    {
        Vector3[] vertices = mesh.vertices;
        Matrix3x3 r = Rotate(angle * Time.deltaTime);
        for (int i = 0; i < vertices.Length; i++)
        {
            vertices[i] = r.MultiplyPoint(vertices[i]);
        }
        mesh.vertices = vertices;
       
        mesh.RecalculateBounds();
    }

}

