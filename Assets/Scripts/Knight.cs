using UnityEngine;

public class Knight : MonoBehaviour
{
    public AudioSource steps;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void footStep()
    {
        steps.Play();
        Debug.Log("footstep");
    }
}
