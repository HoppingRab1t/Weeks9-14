using JetBrains.Annotations;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class TankMovement : MonoBehaviour
{
    public Spawner spawner;

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
        
        //gets spawner variables
        spawner = spawner.GetComponent<Spawner>();

        //checks if the tank is destoryed or not
        if (!(spawner.Tank_Health <= 0))
        {
            //sets the rotation oft the turret
            angles = dir - new Vector2(0, 0);
            transform.up = angles;
            pos = transform.position;
        }



    }
    //gets the value of the controller input
    public void OnLook(InputAction.CallbackContext context)
    {
        dir = context.ReadValue<Vector2>();
    }


  
}

