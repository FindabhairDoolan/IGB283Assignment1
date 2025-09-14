using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Collider2D))]
public class HornetSpinMove : MonoBehaviour
{
    public Transform KnobA;
    public Transform KnobB;

    [SerializeField] public float moveSpeed = 3f;
    [SerializeField] public float rotateSpeed = 180f;

    [SerializeField] private InputAction aKey;
    [SerializeField] private InputAction dKey; //Input keys

    private Transform currentTarget;

    private IGB283Vector position;
    private float angleZDeg;

    private Vector3[] originalVertices; //Original mesh vertices


    void Start()
    {
        position = new IGB283Vector(transform.position);
        angleZDeg = transform.eulerAngles.z;
        currentTarget = KnobA;

        Mesh mesh = GetComponent<MeshFilter>().mesh;
        originalVertices = mesh.vertices;
    }

    void Update()
    {
        //knob positions
        IGB283Vector pointA = new IGB283Vector(KnobA.position);
        IGB283Vector pointB = new IGB283Vector(KnobB.position);
        IGB283Vector target = (currentTarget == KnobA) ? pointA : pointB;

        //spin
        angleZDeg += rotateSpeed * Time.deltaTime;
        if (angleZDeg >= 360f) angleZDeg -= 360f;

        //course correct based on movement
        IGB283Vector toTarget = target - position;
        float distSqr = SqrMagnitude(toTarget);

        if (distSqr > 1e-8f)
        {
            IGB283Vector dir = Normalize(toTarget);
            IGB283Vector step = Scale(dir, moveSpeed * Time.deltaTime);

            //transforma
            IGB283Transform T = IGB283Transform.Translation(step.x, step.y, step.z);
            position = T.Apply(position);
        }

        transform.position = position.ToUnityVector();
        transform.rotation = Quaternion.Euler(0f, 0f, angleZDeg);

        changeColour();
        changeScale();
    }

    //change target
    private void FlipTarget()
    {
        currentTarget = (currentTarget == KnobA) ? KnobB : KnobA;
    }

    //bounce of knock
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform == KnobA || other.transform == KnobB)
            FlipTarget();
    }

    //Change hornet colour depending on x position
    private void changeColour()
    {
        Mesh mesh = GetComponent<MeshFilter>().mesh;
        Color[] colors = new Color[mesh.vertexCount]; //List to put new vertex colours in

        float minX = KnobA.position.x;
        float maxX = KnobB.position.x; //Boundary x positions

        for (int i = 0; i < colors.Length; i++) //For all vertices
        {
            float t = Mathf.InverseLerp(minX, maxX, transform.position.x);
            colors[i] = Color.Lerp(Color.red, Color.black, t); //Set colour dependent on the position between boundaries
        }

        mesh.colors = colors; //Update vertex colours
    }

    //Change hornet size depending on x position
    private void changeScale()
    {
        Mesh mesh = GetComponent<MeshFilter>().mesh;
        Vector3[] vertices = new Vector3[originalVertices.Length];//List to put new vertices

        //Determine scaling based on x position
        float minX = KnobA.position.x;
        float maxX = KnobB.position.x;
        float t = Mathf.InverseLerp(minX, maxX, position.x);
        float scaleFactor = Mathf.Lerp(0.5f, 1.5f, t);

        //Create scaling matrix with scaling factor
        IGB283Transform scaleMatrix = IGB283Transform.Scaling(scaleFactor, scaleFactor, 1f);

        //For all vertices
        for (int i = 0; i < vertices.Length; i++)
        {
            IGB283Vector v = new IGB283Vector(originalVertices[i]); //start from original
            v = scaleMatrix.Apply(v); //Apply scaling
            vertices[i] = v.ToUnityVector();
        }

        //Update vertices
        mesh.vertices = vertices;
        mesh.RecalculateBounds();
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

    private void OnEnable()
    {
        // Enable the inputs
        aKey.Enable();
        dKey.Enable();

        // Trigger the hornet speed
        aKey.performed += decreaseSpeed;
        dKey.performed += increaseSpeed;
    }

    private void OnDisable()
    {
        // Disable the inputs
        aKey.Disable();
        dKey.Disable();

        //Stop triggering hornet speed
        aKey.performed -= decreaseSpeed;
        dKey.performed -= increaseSpeed;
    }

    //cooked helper functions(will move them into vector when get chance)
    private static float SqrMagnitude(IGB283Vector v) => v.x * v.x + v.y * v.y + v.z * v.z;

    private static IGB283Vector Normalize(IGB283Vector v)
    {
        float mag = Mathf.Sqrt(v.x * v.x + v.y * v.y + v.z * v.z);
        if (mag < 1e-8f) return new IGB283Vector(0f, 0f, 0f);
        return new IGB283Vector(v.x / mag, v.y / mag, v.z / mag);
    }

    private static IGB283Vector Scale(IGB283Vector v, float s) =>
        new IGB283Vector(v.x * s, v.y * s, v.z * s);
}
