using UnityEngine;

public class CameraRotate : MonoBehaviour
{
    public Transform target;        // El jugador
    public float sensitivity = 50f; // Sensibilidad BAJA
    public float rotationSmooth = 5f;

    private float currentYaw = 0f;

    void LateUpdate()
    {
        if (target == null) return;

        // Movimiento del mouse (solo eje X)
        float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;

        // Acumular rotación
        currentYaw += mouseX;

        // Rotar la cámara alrededor del jugador
        transform.RotateAround(target.position, Vector3.up, mouseX);

        // Mantener la cámara siempre mirando al jugador
        transform.LookAt(target.position + Vector3.up * 1.5f);
    }
}
