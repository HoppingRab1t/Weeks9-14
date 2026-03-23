using UnityEngine;
using UnityEngine.InputSystem;

public class Particles : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public ParticleSystem particles;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            particles.Emit(100);

        }
        if (Mouse.current.rightButton.wasPressedThisFrame)
        {
            particles.Play();

        }
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            particles.Stop();
        }
    }
}
