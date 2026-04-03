using System;
using System.Collections;
using UnityEngine;
public class SpriteAttack : MonoBehaviour
{
    public float type_object;
    public float health;
    public bool Hit = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator Attack()
    {
        float t = 0;
        int repeat = 0;
        while (repeat < 2)
        {
            if (t < 1)
            {
                repeat += 1;
                t = 0;
            }
            t += Time.deltaTime;
            //transform.up

            yield return null;
        }
    }
}
