using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public class Pulse : MonoBehaviour
{
    public Vector2 pos;
    public AnimationCurve curve;
    float time = 0f;
    public int speed = 10;
    public TrailRenderer trailRenderer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        time += speed/10 * Time.deltaTime;
        if (time > 1)
        {
            time = 0;
        }

        pos = transform.position;
        pos.x += speed * Time.deltaTime;

        if (pos.x >= 10)
        {
            trailRenderer.Clear();
            pos.x = -10; 
            transform.position = pos;

            trailRenderer.Clear();


        }
        pos.y = curve.Evaluate(time)*5;

        transform.position = pos;
    }
}
