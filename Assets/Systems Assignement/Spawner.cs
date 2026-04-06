using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class Spawner : MonoBehaviour
{
    //for the scene objects
    public GameObject prefab;
    public GameObject target;
    public Transform parent;

    //for the bullet objects
    public GameObject prefab1;
    public GameObject target1;
    public Transform parent1;


    //tank hitbox
    public SpriteRenderer tank;



    public TankMovement tankmovement;

    public float timer;
    public float Tank_Health;

    Vector3 pos;
    public Vector3 newRot;

    public List<GameObject> object_list;
    public SpriteAttack Scene_Object;

    public List<GameObject> bullet_list;
    public Bullet bullets;




    float points = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    Vector2 bottomLeft;

    void Start()
    {
        newRot = transform.eulerAngles;
        pos = transform.position;
        bottomLeft = Camera.main.ScreenToWorldPoint(new Vector2(0, 0));

    }

    // Update is called once per frame
    void Update()
    {
        //position of the thing
        pos.y += -1.4f * Time.deltaTime;
        
        //dir.x += newRot.x * Time.deltaTime;
        pos.x -= newRot.x ;


        timer += 1 * Time.deltaTime;

        if (timer > 1)
        {
            prefab = Instantiate(target, new Vector2(Random.Range(-10 - (15*newRot.x), 10 + (15*newRot.x)), 7), transform.rotation, parent);
            object_list.Add(prefab);

            for (int i = 0; i < object_list.Count; i += 1)
            {
                Scene_Object = object_list[i].GetComponent<SpriteAttack>();


                if (tank.bounds.Contains(Scene_Object.transform.position))
                {
                    StartCoroutine(hit());
                    Debug.Log("hit");
                }


                if (Scene_Object.transform.position.y < bottomLeft.y - 5) // destroys the object if it goes off screen
                {
                    GameObject current_Object = object_list[i];
                    object_list.Remove(current_Object);
                    Destroy(current_Object);
                }
                if (Scene_Object.health <= 0)
                {
                    if (Scene_Object.type_object == 1)
                    {
                        points = Mathf.RoundToInt(Random.Range(10,20));
                    }
                    if (Scene_Object.type_object == 2)
                    {
                        points = Mathf.RoundToInt(Random.Range(20, 100));

                    }
                    if (Scene_Object.type_object == 3)
                    {
                        points = Mathf.RoundToInt(Random.Range(10, 20));

                    }
                }
            }


            timer = 0;
        }
        transform.eulerAngles = newRot;
        transform.position = pos;
        //transform.up = dir;
        //control k and then d
    }

    //note only put one unity input system in the game else it ignores the others
    public void Move(InputAction.CallbackContext context)
    {
        newRot = context.ReadValue<Vector2>() * Time.deltaTime;
        

    }
    public void OnShoot(InputAction.CallbackContext context)
    {
        int delays= 0;
        

        if (context.performed == true)
        {
            tankmovement = tankmovement.GetComponent<TankMovement>();


            prefab1 = Instantiate(target1, tankmovement.pos,transform.rotation, parent1);
            bullet_list.Add(prefab1);

            for (int i = 0; i < bullet_list.Count; i += 1)
            {
                if (i == bullet_list.Count)
                {
                    bullets = bullet_list[i].GetComponent<Bullet>();

                    bullets.dir = tankmovement.angles;
                    bullets.type = Weapontype;
                }
            }


        }
    }

    int Weapontype = 1;
    public void OnNext(InputAction.CallbackContext context)
    {
        if (context.performed == true)
        {
            Weapontype = 1;
        }
        
    }
    public void OnPrevious(InputAction.CallbackContext context)
    {
        if (context.performed == true)
        {
            Weapontype = 2;
        }

    }
    IEnumerator hit()
    {
        float t = 0;
        
        while (!tank.bounds.Contains(Scene_Object.transform.position))
        {
            t += Time.deltaTime;
            if (t > 1)
            {
                Scene_Object.health -= 5;
                Tank_Health -= 1;
                t = 0;
            }
            yield return null;
        }

    }



}
