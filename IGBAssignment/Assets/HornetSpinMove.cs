using UnityEngine;

[DisallowMultipleComponent]
public class HornetSpinMove : MonoBehaviour
{
    // editable in Inspector
    [UnityEngine.SerializeField] Vector3 pointA = new Vector3(-2, 0, 0);
    [UnityEngine.SerializeField] Vector3 pointB = new Vector3(2, 0, 0);
    [UnityEngine.SerializeField] float slideSpeed = 1.0f;
    [UnityEngine.SerializeField] float rotationSpeed = 90f;

    // handle visuals
    [UnityEngine.SerializeField] float handleSize = 0.35f;
    [UnityEngine.SerializeField] Material handleMaterial;

    Mesh mesh;
    Vector3[] baseVertices;
    bool initialised;

    enum Dragging { None, A, B }
    Dragging dragging = Dragging.None;
    float dragDepth;

    // handle transforms
    Transform handleAT, handleBT;

    // -- Create a small square (quad) we can click/drag
    void CreateHandle(ref Transform outTransform, string name, Vector3 position)
    {
        var go = GameObject.CreatePrimitive(PrimitiveType.Quad); // includes collider
        go.name = name;
        go.transform.SetParent(this.transform, false);
        go.transform.position = position;
        go.transform.localScale = Vector3.one * handleSize;
        if (handleMaterial) go.GetComponent<MeshRenderer>().material = handleMaterial;
        outTransform = go.transform;
    }

    void Update()
    {
        // --- Lazy init (wait for the mesh to exist and be filled) ---
        if (!initialised)
        {
            var mf = GetComponent<MeshFilter>();
            if (mf == null || mf.sharedMesh == null) return;
            mesh = mf.mesh;
            if (mesh.vertexCount == 0) return;

            baseVertices = mesh.vertices;

            // create draggable squares at A and B
            CreateHandle(ref handleAT, "HandleA", pointA);
            CreateHandle(ref handleBT, "HandleB", pointB);

            initialised = true;
        }

        // --- Mouse dragging (vertical only) ---
        var cam = Camera.main;
        if (cam != null)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (Physics.Raycast(cam.ScreenPointToRay(Input.mousePosition), out var hit))
                {
                    if (hit.transform == handleAT) { dragging = Dragging.A; dragDepth = hit.distance; }
                    else if (hit.transform == handleBT) { dragging = Dragging.B; dragDepth = hit.distance; }
                }
            }

            if (Input.GetMouseButton(0) && dragging != Dragging.None)
            {
                var t = dragging == Dragging.A ? handleAT : handleBT;
                Vector3 world = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, dragDepth));
                t.position = new Vector3(t.position.x, world.y, t.position.z); // constrain to Y
                if (dragging == Dragging.A) pointA = t.position; else pointB = t.position;
            }

            if (Input.GetMouseButtonUp(0)) dragging = Dragging.None;
        }

        // keep handles in sync if edited from Inspector
        if (dragging == Dragging.None)
        {
            if (handleAT) handleAT.position = pointA;
            if (handleBT) handleBT.position = pointB;
        }

        // --- Slide between A and B + rotate using your custom math ---
        float tParam = Mathf.PingPong(Time.time * slideSpeed, 1f);

        IGB283Vector A = new IGB283Vector(pointA);
        IGB283Vector B = new IGB283Vector(pointB);
        IGB283Vector P = new IGB283Vector(
            A.x * (1f - tParam) + B.x * tParam,
            A.y * (1f - tParam) + B.y * tParam,
            A.z * (1f - tParam) + B.z * tParam
        );

        float angle = rotationSpeed * Time.time;

        IGB283Transform TR =
            IGB283Transform.Translation(P.x, P.y, P.z) *
            IGB283Transform.RotationZ(angle);

        var verts = new Vector3[baseVertices.Length];
        for (int i = 0; i < baseVertices.Length; i++)
            verts[i] = TR.Apply(new IGB283Vector(baseVertices[i])).ToUnityVector();

        mesh.vertices = verts;
        mesh.RecalculateBounds();
    }
}
