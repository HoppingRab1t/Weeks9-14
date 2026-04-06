using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Vector2 dir;
    public int type;
    Vector3 angles;

    
    public List <Gradient> trailcolor;
    public List <Color> spritecolors;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        TrailRenderer trail = GetComponent<TrailRenderer>();
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

        angles = dir - new Vector2(0, 0);
        transform.up = angles;
        Debug.Log(type);

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
    void Update()
    {
        
        times += Time.deltaTime;
        if (type == 1)
        {
            transform.position += transform.up * 35 * Time.deltaTime;

        }
        else
        {
            transform.position += transform.up * 15 * Time.deltaTime;

        }

    }
}
