using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Collider2D))]
public class HornetSpinMove : MonoBehaviour
{
    public Transform PointA;
    public Transform PointB;

    [SerializeField] public float moveSpeed = 3f;
    [SerializeField] public float rotateSpeed = 180f;

    //Input keys
    [SerializeField] private InputAction pKey;
    [SerializeField] private InputAction spaceKey;
    [SerializeField] private InputAction aKey;
    [SerializeField] private InputAction dKey;

    //Knife settings
    public GameObject knife;
    public Transform knifeSpawnPoint;
    private bool queueKnifeSpawn = false;

    private Transform currentTarget;

    private IGB283Vector position;
    private float angleZDeg;

    //Original mesh vertices
    private Vector3[] originalVertices; 

    //toggle state
    private bool showingPseudoCrossProduct = false; 

    void Start()
    {
        position = new IGB283Vector(transform.position);
        angleZDeg = transform.eulerAngles.z;
        currentTarget = PointA;

        Mesh mesh = GetComponent<MeshFilter>().mesh;
        originalVertices = mesh.vertices;

    }

    void Update()
    {
        //point positions
        IGB283Vector pointA = new IGB283Vector(PointA.position);
        IGB283Vector pointB = new IGB283Vector(PointB.position);
        IGB283Vector target = (currentTarget == PointA) ? pointA : pointB;

        //spin
        angleZDeg += rotateSpeed * Time.deltaTime;
        if (angleZDeg >= 360f) angleZDeg -= 360f;

        //course correct based on movement
        IGB283Vector toTarget = target - position;
        float distSqr = IGB283Vector.SqrMagnitude(toTarget);

        if (distSqr > 1e-8f)
        {
            IGB283Vector dir = IGB283Vector.Normalize(toTarget);
            IGB283Vector step = IGB283Vector.Scale(dir, moveSpeed * Time.deltaTime);
            IGB283Transform T = IGB283Transform.Translation(step.x, step.y, step.z);
            position = T.Apply(position);
        }

        //rotate scale and move
        changeTransformScale();

        if (showingPseudoCrossProduct)
            applyPseudoCrossProduct();
        else
            changeColour();

        //make knives
        if (queueKnifeSpawn)
        {
            //First line roates the knife spawner in the same way as the hornetmesh
            Quaternion rotation = Quaternion.Euler(0f, 0f, angleZDeg);
            //Hornet mesh is the spawnPosition, and the knife is spawned there
            Vector3 spawnPosition = position.ToUnityVector(); 
            Instantiate(knife, spawnPosition, rotation);
            queueKnifeSpawn = false;
        }

        //a distance based flip in case the weird mesh makes flip not work
        if (IGB283Vector.SqrMagnitude(toTarget) <= 0.01f)
        {
            FlipTarget();
        }
    }

    //change target
    private void FlipTarget()
    {
        currentTarget = (currentTarget == PointA) ? PointB : PointA;
    }

    //bounce off point
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Only flip when we hit the CURRENT target, ignore the other point
        if (other.transform == currentTarget)
        {
            FlipTarget();
        }

    }

    //Change hornet colour depending on x position
    private void changeColour()
    {
        Color black = new Color(0.0f, 0.0f, 0.0f, 1f);
        Color red = new Color(0.459f, 0.051f, 0.051f, 1f);

        Mesh mesh = GetComponent<MeshFilter>().mesh;
        Color[] colours = new Color[mesh.vertexCount]; //List to put new vertex colours in

        float minX = PointA.position.x;
        float maxX = PointB.position.x; //Boundary x positions

        for (int i = 0; i < colours.Length; i++) //For all vertices
        {
            float t = Mathf.InverseLerp(minX, maxX, position.x);
            colours[i] = Color.Lerp(black, red, t); //Set colour dependent on the position between boundaries
        }

        mesh.colors = colours; //Update vertex colours
    }

    private void OnEnable()
    {
        // Enable the inputs
        aKey.Enable();
        dKey.Enable();
        pKey.Enable();
        spaceKey.Enable();

        // Trigger the events
        aKey.performed += decreaseSpeed;
        dKey.performed += increaseSpeed;
        pKey.performed += showPseudoCrossProduct;
        spaceKey.performed += throwKnife;
    }

    private void OnDisable()
    {
        // Disable the inputs
        aKey.Disable();
        dKey.Disable();
        pKey.Disable();
        spaceKey.Disable();

        //Stop triggering the events
        aKey.performed -= decreaseSpeed;
        dKey.performed -= increaseSpeed;
        pKey.performed -= showPseudoCrossProduct;
        spaceKey.performed -= throwKnife;
    }

    //Queue up a knife to be thrown (done in update function to avoid drifting spawn)
    private void throwKnife(InputAction.CallbackContext context)
    {
        queueKnifeSpawn = true;
    }

    //Increase hornet move speed
    private void increaseSpeed(InputAction.CallbackContext context)
    {
        moveSpeed += 1f;
    }

    //Decrease hornet move speed
    private void decreaseSpeed(InputAction.CallbackContext context)
    {
        moveSpeed = Mathf.Max(0f, moveSpeed - 1f);
    }

    private void applyPseudoCrossProduct()
    {
        //Get mesh
        Mesh mesh = GetComponent<MeshFilter>().mesh;
        Vector3[] vertices = mesh.vertices;
        int[] triangles = mesh.triangles;
        Color[] colours = new Color[vertices.Length];

        //Get camera direction
        Vector3 cameraDirection = Camera.main.transform.forward;

        for (int i = 0; i < triangles.Length; i += 3) //For every triangle in mesh
        {
            //Get triangle vertices
            Vector3 a = vertices[triangles[i]];
            Vector3 b = vertices[triangles[i + 1]];
            Vector3 c = vertices[triangles[i + 2]];

            //Determine triangle edges
            Vector3 s0 = b - a;
            Vector3 s1 = c - a;

            //Determine normal
            Vector3 normal = Vector3.Cross(s1, s0);

            //Rotate normal
            Vector3 rotatedNormal = transform.rotation * normal;

            //Dot the normal with the camera direction
            float dotProduct = Vector3.Dot(rotatedNormal, cameraDirection);

            //Positive dot product is front facing = green, back facing is red
            Color faceColour = (dotProduct > 0f) ? Color.green : Color.red;

            //Apply colour to the vertices of the triangle
            colours[triangles[i]] = faceColour;
            colours[triangles[i + 1]] = faceColour;
            colours[triangles[i + 2]] = faceColour;
        }

        mesh.colors = colours; //Update colours
    }

    private void showPseudoCrossProduct(InputAction.CallbackContext context)
    {
        showingPseudoCrossProduct = !showingPseudoCrossProduct;
    }

    //One function to change Hornet's size based on x position while also moving Hornet and rotating her
    private void changeTransformScale()
    {
        Mesh mesh = GetComponent<MeshFilter>().mesh;
        Vector3[] verts = new Vector3[originalVertices.Length];

        //calculate the pivot
        Vector3 pivot = Vector3.zero;
        for (int i = 0; i < originalVertices.Length; i++)
            pivot += originalVertices[i];
        pivot /= Mathf.Max(1, originalVertices.Length);

        //Determine scaling based on x position
        float minX = PointA.position.x;
        float maxX = PointB.position.x;
        float t = Mathf.InverseLerp(minX, maxX, position.x);
        float scale = Mathf.Lerp(0.5f, 1.5f, t);

        //2D rotation about Z applied in space around pivot
        float rad = angleZDeg * Mathf.Deg2Rad;
        float s = Mathf.Sin(rad);
        float c = Mathf.Cos(rad);

        //Calculate offset between desired position and hornet's original position.
        Vector3 localOffset = position.ToUnityVector() - transform.position;

        for (int i = 0; i < verts.Length; i++)
        {
            Vector3 v0 = originalVertices[i];

            //translate to pivot
            float px = v0.x - pivot.x;
            float py = v0.y - pivot.y;
            float pz = v0.z - pivot.z;

            //uniform scale in XY
            px *= scale;
            py *= scale;

            //rotate in XY
            float rx = px * c - py * s;
            float ry = px * s + py * c;
            float rz = pz;

            //translate back from pivot, apply translation offset
            verts[i] = new Vector3(rx + pivot.x, ry + pivot.y, rz + pivot.z) + localOffset;
        }
        //Update vertices with scaling change, translation change, and rotation change
        mesh.vertices = verts;
        mesh.RecalculateBounds();
    }
}
