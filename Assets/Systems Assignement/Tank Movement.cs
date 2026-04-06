using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class TankMovement : MonoBehaviour
{
    float rotation;
    public Vector2 dir;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //converts the x and y into a direction
        Vector3 angles = dir - (Vector2)transform.position;
        transform.up = angles;
    }
    void change_direciton()
    {
        //Vector2
        
    }
    public void OnLook(InputAction.CallbackContext context)
    {
          dir = context.ReadValue<Vector2>();
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        Debug.Log("attack" + context.phase);
        if (context.performed == true)
        {
            //SFX.Play();
        }
    }
    public void OnPoint(InputAction.CallbackContext context)
    {
        //movement = Camera.main.ScreenToWorldPoint(context.ReadValue<Vector2>());

    }
}

