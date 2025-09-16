using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Test : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {
        // Create the three matrices
        Matrix3x3 m1 = new Matrix3x3();
        m1.SetRow(0, new Vector3(3.0f, 12.0f, 4.0f));
        m1.SetRow(1, new Vector3(5.0f, 6.0f, 8.0f));
        m1.SetRow(2, new Vector3(1.0f, 0.0f, 2.0f));

        Matrix3x3 m2 = new Matrix3x3();
        m2.SetRow(0, new Vector3(7.0f, 3.0f, 8.0f));
        m2.SetRow(1, new Vector3(11.0f, 9.0f, 5.0f));
        m2.SetRow(2, new Vector3(6.0f, 8.0f, 4.0f));

        Matrix3x3 m3 = new Matrix3x3();
        m3.SetRow(0, new Vector3(3.0f, 0.0f, 2.0f));
        m3.SetRow(1, new Vector3(2.0f, 0.0f, -2.0f));
        m3.SetRow(2, new Vector3(0.0f, 1.0f, 1.0f));

        float c1 = 2.0f;

        // Output values to the console
        Debug.Log("m1 = \r\n" + m1);
        Debug.Log("m2 = \r\n" + m2);
        Debug.Log("m3 = \r\n" + m3);
        Debug.Log("c1 = " + c1);

        // Perform the operations on the matrices
        // Test equivelance
        Matrix3x3 m1Equal = new Matrix3x3();
        m1Equal.SetRow(0, new Vector3(3.0f, 12.0f, 4.0f));
        m1Equal.SetRow(1, new Vector3(5.0f, 6.0f, 8.0f));
        m1Equal.SetRow(2, new Vector3(1.0f, 0.0f, 2.0f));

        if (m1.Equals(m1Equal))
        {
            Debug.Log("True m1.Equals(m1Equal)");
        }
        else
        {
            Debug.Log("False m1.Equals(m1Equal) (This may cause all other tests to fail)");
        }

        // Perform the operations on the matrices
        // Test equivelance
        Matrix3x3 m1NotEqual = new Matrix3x3();
        m1NotEqual.SetRow(0, new Vector3(3.0f, 12.0f, 4.0f));
        m1NotEqual.SetRow(1, new Vector3(5.0f, 6.0f, 55.0f));
        m1NotEqual.SetRow(2, new Vector3(1.0f, 0.0f, 2.0f));

        if (!m1.Equals(m1NotEqual))
        {
            Debug.Log("True !m1.Equals(m1NotEqual)");
        }
        else
        {
            Debug.Log("False !m1.Equals(m1NotEqual) (This may cause all other tests to fail)");
        }

        // Test identity
        Matrix3x3 mIdentity = Matrix3x3.Identity;
        Matrix3x3 mIdentityTest = new Matrix3x3();
        mIdentityTest.SetRow(0, new Vector3(1.0f, 0.0f, 0.0f));
        mIdentityTest.SetRow(1, new Vector3(0.0f, 1.0f, 0.0f));
        mIdentityTest.SetRow(2, new Vector3(0.0f, 0.0f, 1.0f));

        Debug.Log(mIdentity.Equals(mIdentityTest) + " I3' = \r\n" + mIdentity);

        // Transpose m1
        Matrix3x3 m1Transposed = m1.Transpose;
        Matrix3x3 m1TransposedTest = new Matrix3x3();
        m1TransposedTest.SetRow(0, new Vector3(3.0f, 5.0f, 1.0f));
        m1TransposedTest.SetRow(1, new Vector3(12.0f, 6.0f, 0.0f));
        m1TransposedTest.SetRow(2, new Vector3(4.0f, 8.0f, 2.0f));

        Debug.Log(m1Transposed.Equals(m1TransposedTest) + " m1' = \r\n" + m1Transposed);

        // Multiply m1 and m2 together
        Matrix3x3 m1xm2 = m1 * m2;
        Matrix3x3 m1xm2Test = new Matrix3x3();
        m1xm2Test.SetRow(0, new Vector3(177.0f, 149.0f, 100.0f));
        m1xm2Test.SetRow(1, new Vector3(149.0f, 133.0f, 102.0f));
        m1xm2Test.SetRow(2, new Vector3(19.0f, 19.0f, 16.0f));

        Debug.Log(m1xm2.Equals(m1xm2Test) + " m1 x m2 = \r\n" + m1xm2);

        // Multiply m2 and m1 together
        Matrix3x3 m2xm1 = m2 * m1;
        Matrix3x3 m2xm1Test = new Matrix3x3();
        m2xm1Test.SetRow(0, new Vector3(44.0f, 102.0f, 68.0f));
        m2xm1Test.SetRow(1, new Vector3(83.0f, 186.0f, 126.0f));
        m2xm1Test.SetRow(2, new Vector3(62.0f, 120.0f, 96.0f));

        Debug.Log(m2xm1.Equals(m2xm1Test) + " m2 x m1 = \r\n" + m2xm1);

        // Multiply m3 by a scalar
        Matrix3x3 c1xm3 = c1 * m3;
        Matrix3x3 c1xm3Test = new Matrix3x3();
        c1xm3Test.SetRow(0, new Vector3(6.0f, 0.0f, 4.0f));
        c1xm3Test.SetRow(1, new Vector3(4.0f, 0.0f, -4.0f));
        c1xm3Test.SetRow(2, new Vector3(0.0f, 2.0f, 2.0f));

        Debug.Log(c1xm3.Equals(c1xm3Test) + " c1 x m3 = \r\n" + c1xm3);
    }

    // Update is called once per frame
    void Update()
    {

    }

}
