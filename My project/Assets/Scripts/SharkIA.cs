using UnityEngine;

public class SharkOrbit : MonoBehaviour
{
    public Transform centerPoint;   // El centro de la isla
    public float radius = 20f;      // Qué tan lejos gira
    public float speed = 5f;        // Velocidad de rotación
    public float height = -2f;      // Altura del tiburón (negativa si está bajo agua)

    private float angle = 0f;

    void Update()
    {
        if (centerPoint == null) return;

        angle += speed * Time.deltaTime;

        float x = Mathf.Cos(angle) * radius;
        float z = Mathf.Sin(angle) * radius;

        Vector3 newPos = new Vector3(centerPoint.position.x + x,
                                     centerPoint.position.y + height,
                                     centerPoint.position.z + z);

        transform.position = newPos;

        // Mirar hacia adelante en la órbita
        transform.LookAt(centerPoint.position);
    }
}
