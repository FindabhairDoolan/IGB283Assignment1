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
        spriteRenderer = GetComponent<SpriteRenderer>();
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
        qKey.Enable();
        wKey.Enable();
        mouseClick.Enable();
        qKey.performed += ColourKnobWhite;
        wKey.performed += ColourKnobBlue;
        mouseClick.performed += MouseClickAction;
        mouseClick.canceled += MouseReleaseAction;
    }

    private void OnDisable()
    {
        qKey.Disable();
        wKey.Disable();
        mouseClick.Disable();
        qKey.performed -= ColourKnobWhite;
        wKey.performed -= ColourKnobBlue;
        mouseClick.performed -= MouseClickAction;
        mouseClick.canceled -= MouseReleaseAction;
    }

    private Vector3 GetMousePosition()
    {
        //Get the mouse position with appropriate z coordinate
        Vector3 mouseInput = Mouse.current.position.ReadValue();
        mouseInput.z = transform.position.z -
        Camera.main.transform.position.z;

        Vector3 mouseInWorld =
        Camera.main.ScreenToWorldPoint(mouseInput);
        return mouseInWorld;
    }

    private void MouseClickAction(InputAction.CallbackContext context)
    {
        Vector3 mouseInWorld = GetMousePosition();
        Collider2D hitCollider = Physics2D.OverlapPoint(mouseInWorld);
        if (hitCollider != null && hitCollider.TryGetComponent(out Knob
        knob))
        {
            knob.IsMoving = true;
        }
    }

    private void MouseReleaseAction(InputAction.CallbackContext
    context)
    {
        IsMoving = false;
    }

    private void Move()
    {
        // Move the knob to the mouse, if allowed
        if (IsMoving)
            transform.position = GetMousePosition();
    }


}
