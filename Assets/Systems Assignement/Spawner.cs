using NUnit.Framework;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Spawner : MonoBehaviour
{
    public GameObject prefab;
    public GameObject target;
    public Transform parent;

    public float timer;

    Vector2 pos;
    Vector2 direciton;
    public List<GameObject> object_list;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        pos = transform.position;

        //position of the thing
        pos.y += -1 * Time.deltaTime;

        timer += 1 * Time.deltaTime;

        if (timer > 2)
        {
            prefab = Instantiate(target, new Vector2(Random.Range(-10, 10), 4), transform.rotation, parent);
            object_list.Add(prefab);

            for (int i = 0; i < object_list.Count; i += 1)
            {

            }


            timer = 0;
        }

        transform.position = pos;
        transform.up = direciton;
        //control k and then d
    }
    public void OnPoint(InputAction.CallbackContext context)
    {
        Vector2 direciton = Camera.main.ScreenToWorldPoint(context.ReadValue<Vector2>());
        Debug.Log("poo");
    }
}
