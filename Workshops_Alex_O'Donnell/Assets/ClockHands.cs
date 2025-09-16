using UnityEngine;

public class ClockHands : MonoBehaviour
{
    [SerializeField] private Material material;
    [SerializeField] private float rotationTime = 10;
    [SerializeField] private float length = 1;
    [SerializeField] private float width = 1;
    [SerializeField] private Color colour = Color.red;


    private Mesh mesh;
    // Start is called once before the first execution of Update after

    void Start()
    {
        mesh = gameObject.AddComponent<MeshFilter>().mesh;
        
        mesh.vertices = new Vector3[]
        {
        new Vector3(0, 0, 0),
        new Vector3(0, length, 0),
        new Vector3(width, length, 0),
        new Vector3(width, 0, 0),
        
        };

        Color triColour = colour;
        mesh.colors = new Color[]
        {
        triColour,
        triColour,
        triColour,
        triColour,
        };

        mesh.triangles = new int[] {
            0, 1, 3,
            1, 2, 3,
         };
        
    }
    // Update is called once per frame
    void Update()
    {
        float angle = 2.0f * Mathf.PI/rotationTime;
        RotateTriangle(angle);
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

    private void RotateTriangle(float angle)
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
