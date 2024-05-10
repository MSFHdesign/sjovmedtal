using UnityEngine;
using UnityEngine.InputSystem;

public class PinchGestureDetector : MonoBehaviour
{
    public InputActionAsset inputActions;
    private InputAction touch0Action;
    private InputAction touch1Action;

    private void Awake()
    {
        // Få adgang til de specifikke actions for hver touch position
        touch0Action = inputActions.FindAction("Touch/Touch #0/Position", throwIfNotFound: true);
        touch1Action = inputActions.FindAction("Touch/Touch #1/Position", throwIfNotFound: true);
    }

    private void OnEnable()
    {
        // Aktiver hver action og abonner på deres performed event
        touch0Action.Enable();
        touch1Action.Enable();
        touch0Action.performed += OnTouchPerformed;
        touch1Action.performed += OnTouchPerformed;
    }

    private void OnDisable()
    {
        // Deaktiver hver action og afmeld deres performed event
        touch0Action.Disable();
        touch1Action.Disable();
        touch0Action.performed -= OnTouchPerformed;
        touch1Action.performed -= OnTouchPerformed;
    }

    private void OnTouchPerformed(InputAction.CallbackContext context)
    {
        // Læs positionen for hver touch, når den udløses
        Vector2 touch0Position = touch0Action.ReadValue<Vector2>();
        Vector2 touch1Position = touch1Action.ReadValue<Vector2>();

        Debug.Log($"Pinching: Touch0 at {touch0Position}, Touch1 at {touch1Position}");
    }
}
