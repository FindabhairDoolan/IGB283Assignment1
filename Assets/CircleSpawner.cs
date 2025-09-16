using UnityEngine;

public class CircleSpawner : MonoBehaviour
{
    
    [Header("Wave Bounds")]
    [SerializeField] private float minX = -5f;
    [SerializeField] private float maxX = 5f;
    [SerializeField] private float minY = -3f;
    [SerializeField] private float maxY = 3f;
    
    [Header("Circle Settings")]
    [Min(1)][SerializeField] private int xCount = 15;
    [Min(1)][SerializeField] private int yCount = 15;
    
    [SerializeField] private Circle Circle;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        float xDistance = (maxX - minX) / xCount;
        float yDistance = (maxY - minY) / yCount;
        int i = 0;
        for (int row = 0; row < yCount; row++)
        {
            Vector3 position = Vector3.zero;

            position.y = minY + row * yDistance;

            for (int column = 0; column < xCount; column++)
            {
               
                position.x = minX + column * xDistance;
                
                Circle newCircle = Instantiate(Circle, position, Quaternion.identity);
                newCircle.name = Circle.name + "_" + (i+1).ToString();
                i = i + 1;
                newCircle.MinX = minX;
                newCircle.MaxX = maxX;
                newCircle.MinY = minY;
                newCircle.MaxY = maxY;
            }
        }


    }

        // Update is called once per frame
        void Update()
    {
        
    }
}
