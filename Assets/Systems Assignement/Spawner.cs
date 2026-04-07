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

    public Transform tankRotation;


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
    int steps;
    void Update()
    {
        //position of the thing
        pos.y += -1.4f * Time.deltaTime;

        //dir.x += newRot.x * Time.deltaTime;
        pos.x -= newRot.x;

        tankRotation.eulerAngles = new Vector3(0,0, newRot.x * -1000);

        timer += 1 * Time.deltaTime;

        if (ButtonPressedDown)
        {
            if (Weapontype == 1)
            {
                delays = 0.1f;
            }
            else if (Weapontype == 2)
            {
                delays = 1;

            }

            if (timedelay > delays)
            {
                tankmovement = tankmovement.GetComponent<TankMovement>();


                prefab1 = Instantiate(target1, tankmovement.pos, transform.rotation, parent1);
                bullet_list.Add(prefab1);

                Bullet bullets = prefab1.GetComponent<Bullet>();
                bullets.dir = tankmovement.angles;
                bullets.type = Weapontype;

                if (Weapontype == 1)
                {

                }
                else if (Weapontype == 2)
                {
                    bullets.transform.localScale = new Vector3(3, 3, 3);
                    Debug.Log(bullets.transform.localScale);

                }

                timedelay = 0;


            }
        }
        //spawn in scene object
        if (timer > 1)
        {
            prefab = Instantiate(target, new Vector2(Random.Range(-10 - (15 * newRot.x), 10 + (15 * newRot.x)), 7), transform.rotation, parent);
            object_list.Add(prefab);
            timer = 0;
        }

        //checks object list
        for (int i = 0; i < object_list.Count; i += 1)
        {
            Scene_Object = object_list[i].GetComponent<SpriteAttack>();
            SpriteRenderer objectspriterenderer = object_list[i].GetComponent<SpriteRenderer>();

            if (tank.bounds.Contains(Scene_Object.transform.position))
            {
                StartCoroutine(hit());
            }


            if (Scene_Object.transform.position.y < bottomLeft.y - 5) // destroys the object if it goes off screen
            {
                GameObject current_Object = object_list[i];
                object_list.Remove(current_Object);
                Destroy(current_Object);
            }

            for (int e = 0; e < bullet_list.Count; e += 1)
            {

                bullets = bullet_list[e].GetComponent<Bullet>();

                //if (bullets.type == 1)
                //{
                //    steps = 35;
                //}
                //if (bullets.type == 2)
                //{
                //    steps = 15;
                //}

                //for (int a = 0; a < steps || objectspriterenderer.bounds.Contains(bullets.transform.position); a += 1)
                //{
                //    bullets.transform.position += bullets.transform.up * 1 * Time.deltaTime;

                //}


                if (objectspriterenderer.bounds.Contains(bullets.transform.position))
                {
                    Debug.Log(Scene_Object.health);
                    if (bullets.type == 1)
                    {
                        Scene_Object.health -= 5;
                    }
                    if (bullets.type == 2)
                    {
                        Scene_Object.health -= 10;
                    }


                    GameObject current_Object = bullet_list[e];
                    bullet_list.Remove(current_Object);
                    Destroy(current_Object);

                    if (Scene_Object.health <= 0)
                    {
                        if (Scene_Object.type_object == 1)
                        {
                            points = Mathf.RoundToInt(Random.Range(10, 20));
                        }
                        if (Scene_Object.type_object == 2)
                        {
                            points = Mathf.RoundToInt(Random.Range(20, 100));

                        }
                        if (Scene_Object.type_object == 3)
                        {
                            points = Mathf.RoundToInt(Random.Range(10, 20));

                        }
                        GameObject current_Objects = object_list[i];
                        object_list.Remove(current_Objects);
                        Destroy(current_Objects);

                        Debug.Log(Scene_Object.health);
                    }

                }

            }
        }

        //checks bullets
        for (int i = 0; i < bullet_list.Count; i += 1)
        {

            bullets = bullet_list[i].GetComponent<Bullet>();




            if (bullets.times > 5)
            {
                GameObject current_Object = bullet_list[i];
                bullet_list.Remove(current_Object);
                Destroy(current_Object);
            }


        }
        transform.eulerAngles = newRot;
        transform.position = pos;
        //transform.up = dir;
        //control k and then d
        timedelay += Time.deltaTime;
        //Debug.Log(Weapontype);
    }

    //note only put one unity input system in the game else it ignores the others
    public void Move(InputAction.CallbackContext context)
    {
        newRot = context.ReadValue<Vector2>() * Time.deltaTime;


    }

    float delays = 0;
    float timedelay = 0;
    public int Weapontype = 1;
    bool ButtonPressedDown;

    public void OnShoot(InputAction.CallbackContext context)
    {


        if (context.performed == true)
        {
            ButtonPressedDown = true;
        }
        if (context.performed == false)
        {
            ButtonPressedDown = false;
        }
    }

    public void OnNext(InputAction.CallbackContext context)
    {
        if (context.performed == true)
        {
            Weapontype = 1;
            if (Weapontype == 1)
            {
                Weapontype = 2;
            }
        }
        //Debug.Log("1");

    }
    public void OnPrevious(InputAction.CallbackContext context)
    {
        if (context.performed == true)
        {
            Weapontype = 2;
            if (Weapontype == 2)
            {
                Weapontype = 1;
            }

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
                Debug.Log("hit");

            }
            yield return null;
        }

    }


    //junk
    public void step(int steps, int where)
    {
        for (int e = 0; e < object_list.Count; e += 1)
        {
            SpriteRenderer objectspriterenderer = object_list[e].GetComponent<SpriteRenderer>();


            bullets.transform.position += transform.up * 1 * Time.deltaTime;

        }
    }

}




