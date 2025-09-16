using UnityEngine;

public class Triangle : MonoBehaviour
{
    [SerializeField] private Material material;

    void Start()
    {
        Mesh mesh = gameObject.AddComponent<MeshFilter>().mesh;
        gameObject.AddComponent<MeshRenderer>().material = material;
        mesh.Clear();

        // Define all vertices - calculated by using original vertex map then downscaling it to fit in the [-1,1]
        mesh.vertices = new Vector3[]
        {
            new Vector3(0.125f, -0.125f, 0),
            new Vector3(0.375f, -0.125f, 0),
            new Vector3(0f, 0f, 0),
            new Vector3(0.125f, 0f, 0),
            new Vector3(0.25f, 0f, 0),
            new Vector3(0.375f, 0f, 0),
            new Vector3(-0.125f, 0.125f, 0),
            new Vector3(0f, 0.125f, 0),
            new Vector3(0.125f, 0.125f, 0),
            new Vector3(0.25f, 0.125f, 0),
            new Vector3(0.375f, 0.125f, 0),
            new Vector3(0.5f, 0.125f, 0),
            new Vector3(0f, 0.25f, 0),
            new Vector3(0.125f, 0.25f, 0),
            new Vector3(0.25f, 0.25f, 0),
            new Vector3(0.375f, 0.25f, 0),
            new Vector3(0.125f, 0.375f, 0),
            new Vector3(0.25f, 0.375f, 0),
            new Vector3(0f, 0.5f, 0),
            new Vector3(0.125f, 0.5f, 0),
            new Vector3(0.25f, 0.5f, 0),
            new Vector3(0.375f, 0.5f, 0),
            new Vector3(0f, 0.625f, 0),
            new Vector3(0.125f, 0.625f, 0),
            new Vector3(0.25f, 0.625f, 0),
            new Vector3(0.375f, 0.625f, 0),
            new Vector3(0f, 0.75f, 0),
            new Vector3(0.125f, 0.75f, 0),
            new Vector3(0.25f, 0.75f, 0),
            new Vector3(0.375f, 0.75f, 0),
            new Vector3(0.125f, 0.875f, 0),
            new Vector3(0.25f, 0.875f, 0),
            new Vector3(0.375f, 0.875f, 0),
            new Vector3(0.5f, 0.875f, 0),
            new Vector3(0.25f, 1.0f, 0),
            new Vector3(0.5f, 1.0f, 0)
        };

        //get copy of mesh
        Vector3[] vertCopy = mesh.vertices; 

        //Get bounds
        float minX = float.MaxValue, maxX = float.MinValue;
        float minY = float.MaxValue, maxY = float.MinValue;

        foreach (var v in vertCopy)
        {
            if (v.x < minX) minX = v.x;
            if (v.x > maxX) maxX = v.x;
            if (v.y < minY) minY = v.y;
            if (v.y > maxY) maxY = v.y;
        }

        //Get center
        Vector3 center = new Vector3((minX + maxX) / 2f, (minY + maxY) / 2f, 0f);

        //Recenter vertices
        for (int i = 0; i < vertCopy.Length; i++)
            vertCopy[i] -= center;

        //Update mesh
        mesh.vertices = vertCopy;
        mesh.RecalculateBounds();



        // Default vertices colour
        Color black = new Color(0.0f,0.0f,0.0f,1f);
        Color red = new Color(0.5f, 0.0f, 0.0f, 1f);
        Color white = new Color(1f, 1f, 1f, 1f);
        mesh.colors = new Color[]
        {
            black,
            black,
            black,
            black,
            black,
            black,
            red,
            black,
            black,
            black,
            black,
            red,
            red,
            red,
            red,
            red,
            red,
            red,
            red,
            red,
            red,
            red,
            white,
            white,
            white,
            white,
            white,
            white,
            white,
            white,
            white,
            white,
            white,
            white,
            white,
            white
        };

        // Define Triangles
        mesh.triangles = new int[]
        {
            0,2,3,
            1,4,5,
            3,2,7,
            5,4,9,
            3,7,8,
            5,9,10,
            7,6,12,
            8,7,12,
            9,8,13,
            10,9,14,
            11,10,15,
            8,12,13,
            9,13,14,
            10,14,15,
            13,12,16,
            14,13,16,
            15,14,17,
            17,14,16,
            17,16,19,
            19,16,18,
            20,17,19,
            21,17,20,
            23,19,22,
            20,19,23,
            20,23,24,
            25,20,24,
            23,22,26,
            24,23,27,
            25,24,28,
            27,23,26,
            28,24,27,
            29,25,28,
            27,26,30,
            29,28,32,
            31,27,30,
            33,29,32,
            31,30,34,
            33,32,35,
        };
   
    }
}



