
using UnityEngine;
using UnityEngine.InputSystem;

public class TouchManager : MonoBehaviour
{

    private PlayerInput playerInput;
    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        playerInput.Enable();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
