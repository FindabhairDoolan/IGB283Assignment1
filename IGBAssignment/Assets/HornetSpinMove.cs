using UnityEngine;

public class HornetSpinMove : MonoBehaviour
{
    //so it can be edited easy later
    [SerializeField] Vector3 pointA = new Vector3(-2, 0, 0);
    [SerializeField] Vector3 pointB = new Vector3(2, 0, 0);
    [SerializeField] float slideSpeed = 1.0f;
    [SerializeField] float rotationSpeed = 90f;

    Mesh mesh;
    Vector3[] baseVertices;
    bool firstTime = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //statement to prevent program from attempting to find mesh before mesh is created
        if (firstTime == true)
        {
            mesh = GetComponent<MeshFilter>().mesh;
            baseVertices = mesh.vertices; //get original vertices
            firstTime = false;
        }
        //i love ping pong
        float t = Mathf.PingPong(Time.time * slideSpeed, 1f);

        //vector math :skull:
        IGB283Vector A = new IGB283Vector(pointA);
        IGB283Vector B = new IGB283Vector(pointB);
        IGB283Vector P = new IGB283Vector(
            A.x * (1f - t) + B.x * t,
            A.y * (1f - t) + B.y * t,
            A.z * (1f - t) + B.z * t
        );

        //Rotate
        float angle = rotationSpeed * Time.time;

        //TR variable for translation times roation
        IGB283Transform TR = IGB283Transform.Translation(P.x, P.y, P.z) * IGB283Transform.RotationZ(angle);

        //Applying the original vertices to make sure it dosent kill itself
        var vertices = new Vector3[baseVertices.Length];
        for (int i = 0; i < baseVertices.Length; i++)
        {
            IGB283Vector vector = new IGB283Vector(baseVertices[i]);
            vertices[i] = TR.Apply(vector).ToUnityVector();
        }

        mesh.vertices = vertices;
        mesh.RecalculateBounds();


    }
}
