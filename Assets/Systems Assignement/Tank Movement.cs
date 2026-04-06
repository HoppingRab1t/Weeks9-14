using JetBrains.Annotations;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class TankMovement : MonoBehaviour
{
    public List<GameObject> bullet_list;

    public Vector2 pos;
   

    public Vector3 angles;
    public Vector2 dir;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //converts the x and y into a direction
        //Debug.Log(dir);
        //Debug.Log(transform.position);

        angles = dir - new Vector2(0,0);
        transform.up = angles;
        pos = transform.position;
        
        }
    void change_direciton()
    {
        //Vector2
        
    }
    public void OnLook(InputAction.CallbackContext context)
    {
          dir = context.ReadValue<Vector2>();
    }

    
    public void OnPoint(InputAction.CallbackContext context)
    {
        //movement = Camera.main.ScreenToWorldPoint(context.ReadValue<Vector2>());

    }
}

