using UnityEngine;

public class Straight : MonoBehaviour
{
    [SerializeField] private float speed = 5f;

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
        // Apply the movement speed
        position.x += speed * Time.deltaTime;
        // Update the GameObject's position based on the move
        transform.position = position;
    }
}
