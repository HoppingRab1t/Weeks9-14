using System.Collections;
using UnityEngine;
public class SpriteAttack : MonoBehaviour
{
    public float type_object;
    public float health = 10;
    public bool Hit = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        type_object = (int)Random.Range(1, 4);
        if (type_object == 3)
        {
            type_object = (int)Random.Range(1, 4); //further lowers the chance of becoming a crate
            if (type_object == 3)
            {
                type_object = (int)Random.Range(3, 6); //chooses crate type if it stills rolls as a crate

            }
        }
        if (type_object == 1)
        {
            health = (int)Random.Range(20, 35);
        }
        if (type_object == 2)
        {
            health = (int)Random.Range(55, 75);

        }
        if (type_object == 3)
        {
            health = (int)Random.Range(10, 15);

        }
        if (type_object == 4)
        {
            health = (int)Random.Range(10, 15);

        }
        if (type_object == 5)
        {
            health = (int)Random.Range(10, 15);

        }
        //Debug.Log(health);

        float randomValue = Random.Range(0.3f, 0.5f);

        transform.eulerAngles = new Vector3(0, 0, Random.Range(0, 360));
        transform.localScale = new Vector3(randomValue, randomValue, randomValue);

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
