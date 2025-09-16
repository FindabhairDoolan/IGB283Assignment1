using UnityEngine;

public class Circle : MonoBehaviour
{
    public float MinX = -5f;
    public float MaxX = 5f;
    public float MinY = -3f;
    public float MaxY = 3f;

    private float movementFactorX;
    private float movementFactorY;

    private float startX;
    private float startY;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        float waveWidth = MaxX - MinX;
        float waveHeight = MaxY - MinY;

        movementFactorX = (transform.position.x - MinX) / waveWidth;
        movementFactorY = (transform.position.y - MinY) / waveHeight;

        startX = transform.position.x;
        startY = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

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
