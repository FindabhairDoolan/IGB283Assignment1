using UnityEngine;

public class IGB283Vector
{
    public float x, y, z;

    //Constructor
    public IGB283Vector(float x, float y, float z)
    {
        this.x = x;
        this.y = y;
        this.z = z;
    }

    public IGB283Vector(Vector3 vector)
    {
        this.x = vector.x;
        this.y = vector.y;
        this.z = vector.z;
    }

    public Vector3 ToUnityVector()
    {
        return new Vector3(x, y, z);
    }

    //Addition
    public static IGB283Vector operator +(IGB283Vector a, IGB283Vector b)
    {
        return new IGB283Vector(a.x + b.x, a.y + b.y, a.z + b.z);
    }

    //Subtraction
    public static IGB283Vector operator -(IGB283Vector a, IGB283Vector b)
    {
        return new IGB283Vector(a.x - b.x, a.y - b.y, a.z - b.z);
    }

    //Negation
    public static IGB283Vector operator -(IGB283Vector v)
    {
        return new IGB283Vector(-v.x, -v.y, -v.z);
    }

    //Dot Product
    public static float Dot(IGB283Vector a, IGB283Vector b)
    {
        return a.x * b.x + a.y * b.y + a.z * b.z;
    }

    //Cross Product
    public static IGB283Vector Cross(IGB283Vector a, IGB283Vector b)
    {
        return new IGB283Vector(
            a.y * b.z - a.z * b.y,
            a.z * b.x - a.x * b.z,
            a.x * b.y - a.y * b.x
        );
    }

}
