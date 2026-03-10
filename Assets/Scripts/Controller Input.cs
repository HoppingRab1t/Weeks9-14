using UnityEngine;
using UnityEngine.InputSystem;

public class ControllerInput : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float speed = 5;
    public Vector2 movement;
    public AudioSource SFX;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += (Vector3)  movement * speed * Time.deltaTime;
        //transform.position = movement;
    }
    public void OnMove(InputAction.CallbackContext context)
    {
         movement = transform.right * context.ReadValue<Vector2>();
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        Debug.Log("attack"+context.phase);
        if(context.performed == true)
        {
            SFX.Play();
        }
    }
    public void OnPoint(InputAction.CallbackContext context)
    {
        movement = Camera.main.ScreenToWorldPoint(context.ReadValue<Vector2>());

    }
    public void OnLook(InputAction.CallbackContext context)
    {
        
        transform.eulerAngles = (Vector3)context.ReadValue<Vector2>();
    }
}
