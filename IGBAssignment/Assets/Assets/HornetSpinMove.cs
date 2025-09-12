using UnityEngine;

public class HornetSpinMove : MonoBehaviour
{
    //editable in Inspector
    [UnityEngine.SerializeField] Vector3 pointA = new Vector3(-2, 0, 0);
    [UnityEngine.SerializeField] Vector3 pointB = new Vector3(2, 0, 0);
    [UnityEngine.SerializeField] float slideSpeed = 1.0f;
    [UnityEngine.SerializeField] float rotationSpeed = 90f;

    //handles
    [UnityEngine.SerializeField] float handleSize = 0.35f;
    [UnityEngine.SerializeField] Material handleMaterial;

    Mesh mesh;
    Vector3[] baseVertices;
    bool initialised;

    enum Dragging { None, A, B }
    Dragging dragging = Dragging.None;
    float dragDepth;

    //handle transformaiton
    Transform handleAT, handleBT;

    //handle to drago
    void CreateHandle(ref Transform outTransform, string name, Vector3 position)
    {
        var go = GameObject.CreatePrimitive(PrimitiveType.Quad); //collider
        go.name = name;
        go.transform.SetParent(this.transform, false);
        go.transform.position = position;
        go.transform.localScale = Vector3.one * handleSize;
        if (handleMaterial) go.GetComponent<MeshRenderer>().material = handleMaterial;
        outTransform = go.transform;
    }

    void Update()
    {
        //ensures this is done after mesh is made
        if (!initialised)
        {
            var mf = GetComponent<MeshFilter>();
            if (mf == null || mf.sharedMesh == null) return;
            mesh = mf.mesh;
            if (mesh.vertexCount == 0) return;

            baseVertices = mesh.vertices;

            //creates the handles
            CreateHandle(ref handleAT, "HandleA", pointA);
            CreateHandle(ref handleBT, "HandleB", pointB);

            initialised = true;
        }

        //drag
        var camera = Camera.main;
        if (camera != null)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (Physics.Raycast(camera.ScreenPointToRay(Input.mousePosition), out var hit))
                {
                    if (hit.transform == handleAT) { dragging = Dragging.A; dragDepth = hit.distance; }
                    else if (hit.transform == handleBT) { dragging = Dragging.B; dragDepth = hit.distance; }
                }
            }

            if (Input.GetMouseButton(0) && dragging != Dragging.None)
            {
                var transform = dragging == Dragging.A ? handleAT : handleBT;
                Vector3 world = camera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, dragDepth));
                transform.position = new Vector3(transform.position.x, world.y, transform.position.z); // constrain to Y
                if (dragging == Dragging.A) pointA = transform.position; else pointB = transform.position;
            }

            if (Input.GetMouseButtonUp(0)) dragging = Dragging.None;
        }

        //sync handles if using inspector
        if (dragging == Dragging.None)
        {
            if (handleAT) handleAT.position = pointA;
            if (handleBT) handleBT.position = pointB;
        }

        //PING PONG!
        float transformParam = Mathf.PingPong(Time.time * slideSpeed, 1f);

        IGB283Vector A = new IGB283Vector(pointA);
        IGB283Vector B = new IGB283Vector(pointB);
        IGB283Vector P = new IGB283Vector(
            A.x * (1f - transformParam) + B.x * transformParam,
            A.y * (1f - transformParam) + B.y * transformParam,
            A.z * (1f - transformParam) + B.z * transformParam
        );

        float angle = rotationSpeed * Time.time;

        //transform*rotate
        IGB283Transform TR =
            IGB283Transform.Translation(P.x, P.y, P.z) *
            IGB283Transform.RotationZ(angle);

        var vertices = new Vector3[baseVertices.Length];
        for (int i = 0; i < baseVertices.Length; i++)
            vertices[i] = TR.Apply(new IGB283Vector(baseVertices[i])).ToUnityVector();

        mesh.vertices = vertices;
        mesh.RecalculateBounds();
    }

    //Configure class for duplication
    public void Configure(Vector3 a, Vector3 b, float slide, float rotation)
    {
        //new points
        pointA = a;
        pointB = b;
        slideSpeed = slide;
        rotationSpeed = rotation;

        //moves clone handles to clone handle points
        if (handleAT) handleAT.position = pointA;
        if (handleBT) handleBT.position = pointB;
    }
}



