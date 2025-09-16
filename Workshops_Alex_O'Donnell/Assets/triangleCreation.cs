using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    [SerializeField] private Material material;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Mesh mesh = gameObject.AddComponent<MeshFilter>().mesh;
        gameObject.AddComponent<MeshRenderer>().material = material;

        mesh.Clear();

        mesh.vertices = new Vector3[]
        {
            new Vector3(0, 0, 0),
            new Vector3(0, 1, 0),
            new Vector3(1, 1, 0)
        };
        Color triColour = new Color(0.8f, 0.3f, 0.3f, 1f); 

        mesh.colors = new Color[]
        {
            triColour,
            triColour,
            triColour
        };

        mesh.triangles = new int[] { 0, 1, 2 };
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
