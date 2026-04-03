using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class Spawner : MonoBehaviour
{
    public GameObject prefab;
    public GameObject target;

    public SpriteRenderer tank;

    public Transform parent;
    public SpriteAttack Scene_Object;

    public float timer;
    public float Tank_Health;

    Vector3 pos;
    Vector3 newRot;
    Vector2 dir;

    public List<GameObject> object_list;


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
        pos.y += -1 * Time.deltaTime;
        pos.x = dir.x;


        timer += 1 * Time.deltaTime;

        if (timer > 2)
        {
            prefab = Instantiate(target, new Vector2(Random.Range(-10, 10), 4), transform.rotation, parent);
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
        transform.eulerAngles = dir;
        transform.position = pos;
        //transform.up = dir;
        //control k and then d
    }

    //note only put one unity input system in the game else it ignores the others
    public void Move(InputAction.CallbackContext context)
    {
        dir += context.ReadValue<Vector2>() * Time.deltaTime;
        Debug.Log(dir);

        if (dir.x >= 45)
        {
            dir.x = 45;
            Debug.Log("789");

        }
        else if (dir.x <= -45)
        {
            dir.x = -45;
            Debug.Log("132");

        }

        newRot = dir;
        newRot.y = 0;

    }
    public void OnAttack(InputAction.CallbackContext context)
    {
        Debug.Log("attack" + context.phase);
        
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
