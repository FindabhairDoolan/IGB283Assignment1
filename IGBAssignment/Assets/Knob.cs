using UnityEngine;
using UnityEngine.InputSystem;

public class Point : MonoBehaviour
{
    [SerializeField] private InputAction mouseClick;
    private SpriteRenderer spriteRenderer;
    [HideInInspector] public bool IsMoving = false;

    void Start()
    {
        //get sprite
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        //checks specifically this frame to stop confusion with other points
        if (Mouse.current.leftButton.wasPressedThisFrame)
            MouseClickAction(default);

        if (Mouse.current.leftButton.wasReleasedThisFrame)
            MouseReleaseAction(default);

        Move();
    }

    private void OnEnable()
    {
        mouseClick.performed += MouseClickAction;
        mouseClick.canceled += MouseReleaseAction;
        mouseClick.Enable();
    }

    private void OnDisable()
    {
        mouseClick.Disable();
        mouseClick.performed -= MouseClickAction;
        mouseClick.canceled -= MouseReleaseAction;
    }

    private Vector3 GetMousePosition()
    {
        //Get the mouse position with appropriate z coordinate
        Vector3 mouseInput = Mouse.current.position.ReadValue();
        mouseInput.z = transform.position.z - Camera.main.transform.position.z;
        //find position in world space
        Vector3 mouseInWorld = Camera.main.ScreenToWorldPoint(mouseInput);
        return mouseInWorld;
    }

    private void MouseClickAction(InputAction.CallbackContext context)
    {
        Vector3 mouseInWorld = GetMousePosition();
        Collider2D hitCollider = Physics2D.OverlapPoint(mouseInWorld);
        // Only start moving if this specific point was clicked
        if (hitCollider != null && hitCollider.gameObject == gameObject)
        {
            IsMoving = true;
        }
    }

    private void MouseReleaseAction(InputAction.CallbackContext context)
    {
        IsMoving = false;
    }

    private void Move()
    {
        // Move the point to the mouse
        if (IsMoving)
        {
            Vector3 mousePosition = GetMousePosition();
            transform.position = new Vector3(
                    transform.position.x,  //current X
                    mousePosition.y,       //mouse Y
                    transform.position.z   //current Z
                );
        }
    }
}
