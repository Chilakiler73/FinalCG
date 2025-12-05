using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public CharacterController controller;
    public Animator animator;

    public float speed = 5f;
    public float turnSpeed = 5f;  // menor para suavizar rotación
    public float gravity = -9.81f;

    private Vector3 velocity;

    void Update()
    {
        float x = Input.GetAxis("Horizontal");     // A / D
        float z = Input.GetAxis("Vertical");       // W / S

        //---------------------------------------------------------------------
        // 1. ROTACIÓN SUAVE
        //---------------------------------------------------------------------
        if (x != 0)
        {
            float targetAngle = transform.eulerAngles.y + x * 90f;
            float angle = Mathf.LerpAngle(transform.eulerAngles.y, targetAngle, Time.deltaTime * turnSpeed);
            transform.rotation = Quaternion.Euler(0, angle, 0);
        }

        //---------------------------------------------------------------------
        // 2. MOVIMIENTO HACIA ADELANTE Y ATRÁS
        //---------------------------------------------------------------------
        Vector3 move = transform.forward * z;
        controller.Move(move * speed * Time.deltaTime);

        //---------------------------------------------------------------------
        // 3. ANIMACIONES
        //---------------------------------------------------------------------
        animator.SetFloat("Speed", Mathf.Abs(z));   // 0 = idle, 1 = caminar
        animator.SetFloat("Vspeed", z);             // + adelante, - atrás

        //---------------------------------------------------------------------
        // 4. GRAVEDAD
        //---------------------------------------------------------------------
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}
