using UnityEngine;

public class Spiral : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float radius = 1f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    // Move the GameObject
    private void Move()
    {
        // Get the current position
        Vector3 position = transform.position;
        //Move along the x-axis
        position.x += speed * Time.deltaTime;
        // Move on the y - axis
        position.y = Mathf.Sin(position.x) * radius;
        //Move on the z-axis
        position.z = Mathf.Cos(position.x) * radius;
        //Set the new position
        transform.position = position;
    }
}
