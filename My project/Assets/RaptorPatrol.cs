using UnityEngine;

public class RaptorPatrol : MonoBehaviour
{
    public Transform[] waypoints;
    public float speed = 3f;
    public float rotationSpeed = 6f;
    public float gravity = -9.8f;

    private int currentPoint = 0;
    private CharacterController controller;
    private Vector3 velocity;

    void Start()
    {
        controller = GetComponent<CharacterController>();

        if (waypoints.Length == 0)
        {
            Debug.LogWarning("No hay waypoints asignados.");
        }
    }

    void Update()
    {
        if (waypoints.Length == 0) return;

        // Dirección hacia el siguiente punto
        Vector3 direction = (waypoints[currentPoint].position - transform.position);
        direction.y = 0; // evitar inclinación
        direction.Normalize();

        // ROTACIÓN CORRECTA
        if (direction.magnitude > 0.1f)
        {
            // 180° porque tu modelo está al revés
            Quaternion targetRotation = Quaternion.LookRotation(direction) * Quaternion.Euler(0, 180, 0);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        // MOVIMIENTO + GRAVEDAD
        velocity = direction * speed;
        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);

        // AVANZAR AL SIGUIENTE PUNTO
        if (Vector3.Distance(transform.position, waypoints[currentPoint].position) < 1f)
        {
            currentPoint++;
            if (currentPoint >= waypoints.Length)
                currentPoint = 0;
        }
    }
}
