using UnityEngine;

public class CircleSpawner : MonoBehaviour
{
    // Bounds of the water wave
    [Header("Wave Bounds")]
    [SerializeField] private float minX = -5f;
    [SerializeField] private float maxX = 5f;
    [SerializeField] private float minY = -3f;
    [SerializeField] private float maxY = 3f;

    // Number of circles in each column and row
    [Header("Circle Settings")]
    [Min(1)][SerializeField] private int xCount = 15;
    [Min(1)][SerializeField] private int yCount = 15;

    // The circle prefab
    [SerializeField] private Circle circlePrefab;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Find the per-axis distance between the circles
        float xDistance = (maxX - minX) / xCount;
        float yDistance = (maxY - minY) / yCount;

        // Loop over the rows
        for (int row = 0; row < yCount; row++)
        {
            Vector3 position = Vector3.zero;
            // Determine the row's y position
            position.y = minY + row * yDistance;

            // Loop over the columns
            for (int column = 0; column < xCount; column++)
            {
                // Determine the circle's x position
                position.x = minX + column * xDistance;
                // Instantiate a new circle and set its position
                Circle circleInstance = Instantiate(circlePrefab, position, Quaternion.identity);
                // Set the circle's bounds
                circleInstance.MinX = minX;
                circleInstance.MaxX = maxX;
                circleInstance.MinY = minY;
                circleInstance.MaxY = maxY;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
