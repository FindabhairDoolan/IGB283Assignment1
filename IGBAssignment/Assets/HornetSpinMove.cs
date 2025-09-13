using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class HornetSpinMove : MonoBehaviour
{
    public Transform KnobA;
    public Transform KnobB;

    [SerializeField] public float moveSpeed = 3f;
    [SerializeField] public float rotateSpeed = 180f;

    private Transform currentTarget;

    private IGB283Vector position;
    private float angleZDeg;

    void Start()
    {
        position = new IGB283Vector(transform.position);
        angleZDeg = transform.eulerAngles.z;
        currentTarget = KnobA;
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
    }

    //chance target
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
