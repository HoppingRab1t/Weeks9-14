using UnityEngine;
using UnityEngine.InputSystem;

public class TankMovement : MonoBehaviour
{
    Vector2 xPos;
    float rotation; 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void change_direciton()
    {
        //Vector2
        
    }
    public void OnMove(InputAction.CallbackContext context)
    {
         //= context.ReadValue<Vector2>();
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

