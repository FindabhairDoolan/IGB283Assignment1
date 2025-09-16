using UnityEngine;

public class Circle : MonoBehaviour
{
    // Bounds of the water wave
    public float MinX = -5f;
    public float MaxX = 5f;
    public float MinY = -3f;
    public float MaxY = 3f;

    // Movement factor (based on the y position)
    private float movementFactorX;
    private float movementFactorY;

    // Starting location
    private float startX;
    private float startY;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Calculate the movement factor
        float waveWidth = MaxX - MinX;
        float waveHeight = MaxY - MinY;
        movementFactorX = (transform.position.x - MinX) / waveWidth;
        movementFactorY = (transform.position.y - MinY) / waveHeight;

        // Set the position of the circle
        startX = transform.position.x;
        startY = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    // Move the circle in a circle using sin and cos
    private void Movement()
    {
        Vector3 position = transform.position;
        position.x = startX + Mathf.Cos(movementFactorY
        + movementFactorX * MaxX - Time.time)
        * movementFactorY;
        position.y = startY + Mathf.Sin(movementFactorY
        + movementFactorX * MaxX - Time.time)
        * movementFactorY;
        transform.position = position;
    }
}
