using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;


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


    //tank turret
    public TankMovement tankmovement;

    //tank stats
    public float timer;
    public float Tank_Health = 100;
    float points = 0;


    Vector3 pos;

    //scene x position change
    public Vector3 newRot;

    //list and objects for scene objects
    public List<GameObject> object_list;
    public SpriteAttack Scene_Object;

    //list and objects for bullets
    public List<GameObject> bullet_list;
    public Bullet bullets;

    public Transform tankRotation;

    //UI elements
    public TextMeshProUGUI pointVal;
    public TextMeshProUGUI tankVal;

    //particles 
    public ParticleSystem particles;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    Vector2 bottomLeft;

    void Start()
    {
        //sets variables and starts the corotines
        Tank_Health = 100;
        StartCoroutine(spawn());
        StartCoroutine(hit());
        newRot = transform.eulerAngles;
        pos = transform.position;
        bottomLeft = Camera.main.ScreenToWorldPoint(new Vector2(0, 0));
        particles.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        if (Tank_Health <= 0) // if the tank dies
        {
            particles.Play(); // start particles and stop coroutines
            StopCoroutine(spawn());
            StopCoroutine(hit());
            

            tankVal.text = "Your tank got destroyed!"; // tells the player that the tank is destoryed
            newRot.x = 0;
            particles.Play();

        }
        else
        {
            //Debug.Log(Tank_Health);

            //sets UI text
            pointVal.text = "Points: " + points;
            tankVal.text = "Health: " + Tank_Health;

            //sets tank positon
            tank.transform.position = new Vector3(0, -3.4f, 0);

            //position of the scene
            pos.y += -1.4f * Time.deltaTime;

            //dir.x += newRot.x * Time.deltaTime;
            pos.x -= newRot.x;

            //changes the tank body rotation based on movement
            tankRotation.eulerAngles = new Vector3(0, 0, newRot.x * -1000);

            timer += 1 * Time.deltaTime;

            if (ButtonPressedDown) // if the button is pressed down set delay and shoot
            {
                if (Weapontype == 1)
                {
                    delays = 0.1f;
                }
                else if (Weapontype == 2)
                {
                    delays = 1;

                }

                if (timedelay > delays) // the timer is over spawn the tank and reset the timer.
                {
                    tankmovement = tankmovement.GetComponent<TankMovement>();

                    //spawn in bullets
                    prefab1 = Instantiate(target1, tankmovement.pos, transform.rotation, parent1);
                    bullet_list.Add(prefab1);

                    Bullet bullets = prefab1.GetComponent<Bullet>();
                    bullets.dir = tankmovement.angles;
                    bullets.type = Weapontype;

                    if (Weapontype == 1)
                    {

                    }
                    else if (Weapontype == 2)
                    {//attempted to change the scale of the bullet
                        bullets.transform.localScale = new Vector3(3, 3, 3);
                        Debug.Log(bullets.transform.localScale);

                    }

                    timedelay = 0;


                }
            }


            //checks object list
            for (int i = 0; i < object_list.Count; i += 1)
            {
                
                Scene_Object = object_list[i].GetComponent<SpriteAttack>();
                SpriteRenderer objectspriterenderer = object_list[i].GetComponent<SpriteRenderer>(); //gets varaibles




                if (Scene_Object.transform.position.y < bottomLeft.y - 5) // destroys the object if it goes off screen
                {
                    GameObject current_Object = object_list[i];
                    object_list.Remove(current_Object);
                    Destroy(current_Object);
                }
            //utter peiece of poop

                for (int e = 0; e < bullet_list.Count; e += 1) // runs through the bullet list
                {

                    bullets = bullet_list[e].GetComponent<Bullet>();

                    //unused stuff
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

                    // if the bullet touches the scene object it destroys the bullet and damages the object
                    if (objectspriterenderer.bounds.Contains(bullets.transform.position))  
                    {
                        //changes health
                        if (bullets.type == 1)
                        {
                            Scene_Object.health -= 5;
                        }
                        if (bullets.type == 2)
                        {
                            Scene_Object.health -= 40;
                        }

                        //destroy bullet
                        GameObject current_Object = bullet_list[e];
                        bullet_list.Remove(current_Object);
                        Destroy(current_Object);

                        //if the scene object health is 0 destroy and give points
                        if (Scene_Object.health <= 0)
                        {
                            if (Scene_Object.type_object == 1)
                            {
                                points += (int)Random.Range(100, 200);
                            }
                            if (Scene_Object.type_object == 2)
                            {
                                points += (int)Random.Range(200, 500);

                            }
                            if (Scene_Object.type_object >= 3)
                            {
                                points += (int)Random.Range(100, 200);

                            }
                            GameObject current_Objects = object_list[i];
                            object_list.Remove(current_Objects);
                            Destroy(current_Objects);

                            Debug.Log(Scene_Object.health);
                        }

                    }

                }
            }
        }

        //checks bullets and destroys them if they fly for a period of time (cleans up space)
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
        //changes the postion and resets delay
        transform.eulerAngles = newRot;
        transform.position = pos;


        timedelay += Time.deltaTime;

        //Debug.Log(Weapontype);
    }
    //control k and then d to format

    //note only put one unity input system in the game else it ignores the others (i added 2 input systems before)
    //sets the varaible that changes the x positon of the scene.
    public void Move(InputAction.CallbackContext context)
    {
        newRot = context.ReadValue<Vector2>() * 2 * Time.deltaTime;


    }
    //other variables
    float delays = 0;
    float timedelay = 0;
    public int Weapontype = 1;
    bool ButtonPressedDown;

    //detects if the button is being pressed down
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
    //detects if the button is pressed and sets the vaiable accordingly

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
    //detects if the button is pressed and sets the vaiable accordingly
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
    //detects if the tank hits the building using coroutine
    IEnumerator hit()
    {
        float t = 0;


        while (true)
        {

            for (int i = 0; i < object_list.Count; i += 1)//runs through list
            {
                Scene_Object = object_list[i].GetComponent<SpriteAttack>();//gets the varaibles/properties of the object

                SpriteRenderer objectspriterenderer = object_list[i].GetComponent<SpriteRenderer>();

                if (tank.bounds.Contains(Scene_Object.transform.position))
                {
                    //changes the tank and scene object health
                    Scene_Object.health -= 5 ;
                    Tank_Health -= 15;
                    //attemtped to shake the tank when hits
                    int randomNum = Random.Range(-1, 1);
                    tank.transform.position = new Vector3(0 + randomNum, -3.4f + randomNum, 0); 

                }
                yield return new WaitForSeconds(0.1f);//delay
            }
            t += Time.deltaTime;
            yield return null;

        }
        yield return null;
    }
    //spawns in the buildings and scene objects using coroutines
    IEnumerator spawn()
    {
        while (true)
        {
            prefab = Instantiate(target, new Vector2(Random.Range(-10 - (15 * newRot.x), 10 + (15 * newRot.x)), 7), transform.rotation, parent);
            object_list.Add(prefab);
            yield return new WaitForSeconds(1f);//delay

        }
        yield return null;
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




