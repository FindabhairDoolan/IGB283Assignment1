using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Matrix3x3
{
    private const int matrixOrder = 3;

    // Static Variables
    // The identiy matrix
    public static Matrix3x3 Identity
    {
        get
        {
            return new Matrix3x3(
            new Vector3(1f, 0f, 0f),
            new Vector3(0f, 1f, 0f),
            new Vector3(0f, 0f, 1f));
            throw new System.NotImplementedException();
        }
    }

    // The zero matrix
    public static Matrix3x3 Zero
    {
        get
        {
            return new Matrix3x3(
                Vector3.zero,
                Vector3.zero,
                Vector3.zero
            );
        }
    }

    // Variables
    // The determinant of the matrix
    public float Determinant
    {
        get
        {
            return
                m[0][0] * (m[1][1] * m[2][2] - m[1][2] * m[2][1])
                - m[0][1] * (m[1][0] * m[2][2] - m[1][2] * m[2][0])
                + m[0][2] * (m[1][0] * m[2][1] - m[1][1] * m[2][0]);
        }
        set
        {
            throw new System.NotImplementedException();
        }
    }


    // The inverse of the matrix
    public Matrix3x3 Inverse
    {
        get
        {
            return (1 / Determinant) * (new Matrix3x3(
                new Vector3(
                    (m[1][1] * m[2][2] - m[1][2] * m[2][1]),
                    -(m[1][0] * m[2][2] - m[1][2] * m[2][0]),
                    (m[1][0] * m[2][1] - m[1][1] * m[2][0])
                ),
                new Vector3(
                    -(m[0][1] * m[2][2] - m[0][2] * m[2][1]),
                    (m[0][0] * m[2][2] - m[0][2] * m[2][0]),
                    -(m[0][0] * m[2][1] - m[0][1] * m[2][0])
                ),
                new Vector3(
                    (m[0][1] * m[1][2] - m[0][2] * m[1][1]),
                    -(m[0][0] * m[1][2] - m[0][2] * m[1][0]),
                    (m[0][0] * m[1][1] - m[0][1] * m[1][0])
                )
            ).Transpose);
        }
    }

    // Is the matrix an identity matrix
    public bool IsIdentity
    {
        get
        {
            return this.Equals(Identity);
        }
    }

    // The element at x, y
    public float this[int row, int column]
    {
        get
        {
            return m[row][column];
        }
        set
        {
            Vector3 v = m[row];
            v[column] = value;
            m[row] = v;
        }
    }

    // The transpose of the matrix
    public Matrix3x3 Transpose
    {
        get
        {
            Matrix3x3 transpose = new Matrix3x3();

            // Switch the columns and rows
            for (int i = 0; i < matrixOrder; i++)
            {
                transpose.SetColumn(i, this.GetRow(i));
            }

            return transpose;

            throw new System.NotImplementedException();
        }
    }

    // Constructor
    // Array to contain the vector data
    private Vector3[] m = new Vector3[3];

    // Create a matrix 3x3 with specified values
    public Matrix3x3(Vector3 r1, Vector3 r2, Vector3 r3)
    {
        m[0] = r1;
        m[1] = r2;
        m[2] = r3;
    }

    // Create a matrix 3x3 initialised with zeros
    public Matrix3x3()
    {
        m[0] = Vector3.zero;
        m[1] = Vector3.zero;
        m[2] = Vector3.zero;
    }

    // Public Functions
    // Get a column of the matrix
    public Vector3 GetColumn(int column)
    {
        return new Vector3(m[0][column], m[1][column], m[2][column]);
    }

    // Get a row of the matrix
    public Vector3 GetRow(int row)
    {
        return m[row];
    }

    // Transform a point by this matrix
    public Vector3 MultiplyPoint(Vector3 p)
    {
        p.z = 1.0f;
        Vector3 v = new Vector3(
            m[0][0] * p[0] + m[0][1] * p[1] + m[0][2] * p[2],
            m[1][0] * p[0] + m[1][1] * p[1] + m[1][2] * p[2],
            m[2][0] * p[0] + m[2][1] * p[1] + m[2][2] * p[2]
        );
        return v;
    }

    // Transform a direction by this matrix
    public Vector3 MultiplyPoint3x4(Vector3 p)
    {
        throw new System.NotImplementedException();
    }

    // Set a column of the matrix
    public Vector3 MultiplyVector(Vector3 v)
    {
        throw new System.NotImplementedException();
    }

    // Set a column of the matrix
    public void SetColumn(int index, Vector3 column)
    {
        // Get the three row vectors of the matrix
        Vector3 r1 = m[0];
        Vector3 r2 = m[1];
        Vector3 r3 = m[2];

        // Set the value at index to be the value from the column vector
        r1[index] = column[0];
        r2[index] = column[1];
        r3[index] = column[2];

        // Reassign the rows to the matrix
        m[0] = r1;
        m[1] = r2;
        m[2] = r3;
    }

    // Set a row of the matrix
    public void SetRow(int index, Vector3 row)
    {
        m[index] = row;
    }

    // Sets this matrix to a translation, rotation and scaling matrix
    public void SetTRS(Vector3 pos, Quaternion q, Vector3 s)
    {
        throw new System.NotImplementedException("If you want to use this method you will need to implement it yourself");
    }

    // Return a string of the matrix
    public override string ToString()
    {
        string s = "";
        s = string.Format(
             "{0,-12:0.00000}{1,-12:0.00000}{2,-12:0.00000}\r\n" +
             "{3,-12:0.00000}{4,-12:0.00000}{5,-12:0.00000}\r\n" +
             "{6,-12:0.00000}{7,-12:0.00000}{8,-12:0.00000}\r\n",
            m[0].x, m[0].y, m[0].z,
            m[1].x, m[1].y, m[1].z,
            m[2].x, m[2].y, m[2].z);

        return s;
    }

    // Operators
    // Multiply two matrices together
    public static Matrix3x3 operator *(Matrix3x3 b, Matrix3x3 c)
    {
        Matrix3x3 resultMatrix = new Matrix3x3();
        // Take the dot product of every column of b with every row of c
        for (int i = 0; i < matrixOrder; i++)
        {
            Vector3 row = new Vector3(
            b[i, 0] * c[0, 0] + b[i, 1] * c[1, 0] + b[i, 2] * c[2, 0],
            b[i, 0] * c[0, 1] + b[i, 1] * c[1, 1] + b[i, 2] * c[2, 1],
            b[i, 0] * c[0, 2] + b[i, 1] * c[1, 2] + b[i, 2] * c[2, 2]);
            resultMatrix.SetRow(i, row);
        }

        return resultMatrix;

        throw new System.NotImplementedException();
    }

    // Multiply a matrix by a scalar 
    public static Matrix3x3 operator *(float b, Matrix3x3 c)
    {
        Matrix3x3 resultMatrix = new Matrix3x3();
        // Multiply all components by b
        for (int i = 0; i < matrixOrder; i++)
        {
            for (int j = 0; j < matrixOrder; j++)
            {
                resultMatrix[i, j] = c[i, j] * b;
            }
        }
        return resultMatrix;

        throw new System.NotImplementedException();
    }

    public static Matrix3x3 operator *(Matrix3x3 c, float b)
    {
        return b * c;
    }

    // Test the equality of this matrix and another
    public bool Equals(Matrix3x3 m2)
    {
        // Loop over all elements
        for (int i = 0; i < matrixOrder; i++)
        {
            for (int j = 0; j < matrixOrder; j++)
            {
                if (this[i, j] != m2[i, j])
                    return false;
            }
        }
        // If reaching here, all elements are equal
        return true;

        throw new System.NotImplementedException();
    }

}
