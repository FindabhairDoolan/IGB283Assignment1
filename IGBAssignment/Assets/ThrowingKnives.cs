using UnityEngine;

public class ThrowingKnives : MonoBehaviour
{
    public float speed = 10f; //Knife travel speed
    public float lifetime = 2f; //Life of knife before destroyed
    private Vector3 direction; //Direction the knife should travel

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Set the knife's direction to its local 'up' when spawned, so it is thrown in sync with Hornet's spin
        direction = transform.up.normalized;

        Destroy(gameObject, lifetime); //Destroy knife when lifetime has passed
    }

    // Update is called once per frame
    void Update()
    {
        //Travel in a straight line
        transform.position += direction * speed * Time.deltaTime;
    }
}
