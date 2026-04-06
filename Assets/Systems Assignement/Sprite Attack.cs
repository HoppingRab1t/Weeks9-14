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
        type_object = (int)Random.Range(1, 3);
        if (type_object == 3)
        {
            type_object = (int)Random.Range(1, 3); //further lowers the chance of becoming a crate
            if (type_object == 3)
            {
                type_object = (int)Random.Range(3, 6); //chooses crate type if it stills rolls as a crate

            }
        }
        
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
