using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform straight;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        // Set the camera's position
        Vector3 position = transform.position;
        position.x = straight.position.x;
        transform.position = position;
    }
}
