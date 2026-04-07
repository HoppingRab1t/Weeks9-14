using System.Collections;
using System.Collections.Generic;
using System.Net.Security;
using Unity.Hierarchy;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Vector2 dir;
    public int type;
    Vector3 angles;

    
    public List <Gradient> trailcolor;
    public List <Color> spritecolors;

    public List<GameObject> list;

    Spawner spanwer;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        TrailRenderer trail = GetComponent<TrailRenderer>();
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        Transform transforms = GetComponent<Transform>();

        angles = dir - new Vector2(0, 0);
        transform.up = angles;
        //Debug.Log(type);

        if (type == 1)
        {
            trail.colorGradient = trailcolor[0];
            spriteRenderer.color = spritecolors[0];
        }
        if (type == 2)
        {
            trail.colorGradient = trailcolor[1];
            spriteRenderer.color = spritecolors[1];
            
        }
    }

    // Update is called once per frame
    public float times;
    public float steps;
    void Update()
    {
        
        times += Time.deltaTime;
        if (type == 2)
        {
            transform.position += transform.up * 35 * Time.deltaTime;
        }
        else
        {
            transform.position += transform.up * 15 * Time.deltaTime;

        }
        //Debug.Log(transform.localScale);

        //if (type == 1)
        //{
        //    steps = 35;
        //}
        //if (type == 2)
        //{
        //    steps = 15;
        //}
        //spawner = GetComponent<Spawner>();
        //for (int i = 0; i < list.Count; i += 1)
        //{
        //    Scene_Object = object_list[i].GetComponent<SpriteAttack>();
        //    SpriteRenderer objectspriterenderer = list[i].GetComponent<SpriteRenderer>();
        //    for (int a = 0; a < steps || objectspriterenderer.bounds.Contains(bullets.transform.position); a += 1)
        //    {
        //        transform.position += transform.up * 1 * Time.deltaTime;

        //    }
        //}
        //transform.position +=  transform.up * 1 * Time.deltaTime;

    }
    
}
