using UnityEngine;

public class Triangle : MonoBehaviour
{
    [SerializeField] private Material material;

    void Start()
    {
        Mesh mesh = gameObject.AddComponent<MeshFilter>().mesh;
        gameObject.AddComponent<MeshRenderer>().material = material;
        mesh.Clear();

        // Define all vertices
        mesh.vertices = new Vector3[]
        {
            new Vector3(2,0,0),   
            new Vector3(4,0,0),   
            new Vector3(1,1,0),   
            new Vector3(2,1,0),   
            new Vector3(3,1,0),   
            new Vector3(4,1,0),   
            new Vector3(0,2,0),  
            new Vector3(1,2,0),   
            new Vector3(2,2,0),   
            new Vector3(3,2,0),  
            new Vector3(4,2,0),   
            new Vector3(5,2,0),   
            new Vector3(1,3,0),   
            new Vector3(2,3,0),   
            new Vector3(3,3,0),   
            new Vector3(4,3,0),  
            new Vector3(2,4,0),  
            new Vector3(3,4,0),  
            new Vector3(1,5,0),   
            new Vector3(2,5,0),   
            new Vector3(3,5,0),  
            new Vector3(4,5,0),   
            new Vector3(1,6,0),   
            new Vector3(2,6,0),   
            new Vector3(3,6,0),
            new Vector3(4,6,0),
            new Vector3(1,7,0),  
            new Vector3(2,7,0),  
            new Vector3(3,7,0),   
            new Vector3(4,7,0),
            new Vector3(2,8,0),
            new Vector3(3,8,0),   
            new Vector3(4,8,0),   
            new Vector3(5,8,0),
            new Vector3(3,9,0),  
            new Vector3(5,9,0)    
        };

        // Colour Vetices
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
            24,19,23,
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



