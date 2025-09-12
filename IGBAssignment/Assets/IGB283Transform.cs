using UnityEngine;

public class IGB283Transform
{
    private float[,] m = new float[4, 4];

    //Constructor identity
    public IGB283Transform()
    {
        for (int i = 0; i < 4; i++)
            m[i, i] = 1f;
    }

    //translation
    public static IGB283Transform Translation(float x, float y, float z)
    {
        IGB283Transform translated = new IGB283Transform();
        translated.m[0, 3] = x;
        translated.m[1, 3] = y;
        translated.m[2, 3] = z;
        return translated;
    }

    //scaling
    public static IGB283Transform Scaling(float x, float y, float z)
    {
        IGB283Transform transform = new IGB283Transform();
        transform.m[0, 0] = x;
        transform.m[1, 1] = y;
        transform.m[2, 2] = z;
        return transform;
    }

    //next 3 are roations on each axis
    public static IGB283Transform RotationZ(float degrees)
    {
        float rad = degrees * Mathf.Deg2Rad;
        IGB283Transform rotate = new IGB283Transform();
        rotate.m[0, 0] = Mathf.Cos(rad);
        rotate.m[0, 1] = -Mathf.Sin(rad);
        rotate.m[1, 0] = Mathf.Sin(rad);
        rotate.m[1, 1] = Mathf.Cos(rad);
        return rotate;
    }

    public static IGB283Transform RotationX(float degrees)
    {
        float rad = degrees * Mathf.Deg2Rad;
        IGB283Transform rotate = new IGB283Transform();
        rotate.m[1, 1] = Mathf.Cos(rad);
        rotate.m[1, 2] = -Mathf.Sin(rad);
        rotate.m[2, 1] = Mathf.Sin(rad);
        rotate.m[2, 2] = Mathf.Cos(rad);
        return rotate;
    }

    public static IGB283Transform RotationY(float degrees)
    {
        float rad = degrees * Mathf.Deg2Rad;
        IGB283Transform rotate = new IGB283Transform();
        rotate.m[0, 0] = Mathf.Cos(rad);
        rotate.m[0, 2] = Mathf.Sin(rad);
        rotate.m[2, 0] = -Mathf.Sin(rad);
        rotate.m[2, 2] = Mathf.Cos(rad);
        return rotate;
    }

    //apply transformers to the vector
    public IGB283Vector Apply(IGB283Vector vector)
    {
        float[] r = new float[4];
        float[] vec = { vector.x, vector.y, vector.z, 1f };

        for (int row = 0; row < 4; row++)
        {
            for (int col = 0; col < 4; col++)
                r[row] += m[row, col] * vec[col];
        }

        return new IGB283Vector(r[0], r[1], r[2]);
    }

    //matrix multiplication
    public static IGB283Transform operator *(IGB283Transform a, IGB283Transform b)
    {
        IGB283Transform result = new IGB283Transform();
        for (int i = 0; i < 4; i++)
            for (int j = 0; j < 4; j++)
            {
                result.m[i, j] = 0;
                for (int k = 0; k < 4; k++)
                    result.m[i, j] += a.m[i, k] * b.m[k, j];
            }
        return result;
    }
}

