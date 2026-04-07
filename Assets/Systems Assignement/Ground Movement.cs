using UnityEngine;

public class GroundMovement : MonoBehaviour
{
    public Spawner spawner;
    float positionx;
    Vector2 pos;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //gets the spanwer code (for variables)
        spawner = spawner.GetComponent<Spawner>();

        //checks if the tank has health
        if (!(spawner.Tank_Health <= 0))
        {
            //moves along with the scene
            pos = transform.position;
            pos.x -= spawner.newRot.x;
            pos.y -= 1.4f * Time.deltaTime;

            //resets positon when reaches to a certain point
            if (pos.y <= -16.5)
            {
                pos.y = 6;
            }
            if (pos.x >= 29)
            {
                pos.x = -11;
            }
            if (pos.x <= -29)
            {
                pos.x = 11;
            }
            //changes position
            transform.position = pos;
        }
    }
}
