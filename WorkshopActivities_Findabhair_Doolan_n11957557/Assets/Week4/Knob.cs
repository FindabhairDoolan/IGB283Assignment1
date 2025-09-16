using UnityEngine;
using UnityEngine.InputSystem;

public class Knob : MonoBehaviour
{
    [SerializeField] private InputAction qKey;
    [SerializeField] private InputAction wKey;
    [SerializeField] private InputAction mouseClick;

    private SpriteRenderer spriteRenderer;
    [HideInInspector] public bool IsMoving = false;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Get the knob's sprite renderer
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Put the knob in view of the camera
        Vector3 cameraPosition = Camera.main.transform.position;
        transform.position = cameraPosition + Camera.main.transform.forward * 2f;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void ColourKnobWhite(InputAction.CallbackContext context)
    {
        spriteRenderer.color = Color.white;
    }

    private void ColourKnobBlue(InputAction.CallbackContext context)
    {
        spriteRenderer.color = Color.blue;
    }

    private void OnEnable()
    {
        // Enable the inputs
        qKey.Enable();
        wKey.Enable();
        mouseClick.Enable();

        // Trigger the knob colouring when the actions are performed
        qKey.performed += ColourKnobWhite;
        wKey.performed += ColourKnobBlue;

        // Trigger movement when the mouse is clicked / released
        mouseClick.performed += MouseClickAction;
        mouseClick.canceled += MouseReleaseAction;


    }

    private void OnDisable()
    {
        // Disable the inputs
        qKey.Disable();
        wKey.Disable();
        mouseClick.Disable();

        // Stop triggering the knob colouring when the actions are performed
        qKey.performed -= ColourKnobWhite;
        wKey.performed -= ColourKnobBlue;

        // Stop triggering movement when the mouse is clicked/released
        mouseClick.performed -= MouseClickAction;
        mouseClick.canceled -= MouseReleaseAction;

    }

    private Vector3 GetMousePosition()
    {
        //Get the mouse position with appropriate z coordinate
        Vector3 mouseInput = Mouse.current.position.ReadValue();
        mouseInput.z = transform.position.z - Camera.main.transform.position.z;
        // Find the mouse position in world space
        Vector3 mouseInWorld =
        Camera.main.ScreenToWorldPoint(mouseInput);

        return mouseInWorld;
    }

    private void MouseReleaseAction(InputAction.CallbackContext context)
    {
        IsMoving = false;
    }

    private void MouseClickAction(InputAction.CallbackContext context)
    {
        Vector3 mouseInWorld = GetMousePosition();
        // Look for a knob at the mouse position
        Collider2D hitCollider = Physics2D.OverlapPoint(mouseInWorld);
        if (hitCollider != null && hitCollider.TryGetComponent(out Knob knob))
        {
                knob.IsMoving = true;
        }
    }

    private void Move()
    {
        // Move the knob to the mouse, if allowed
        if (IsMoving)
            transform.position = GetMousePosition();
    }
}
