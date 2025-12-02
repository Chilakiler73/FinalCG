using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    [Header("Cycle Settings")]
    public float dayLength = 60f; // segundos para un día entero
    public Light sunLight;
    public Gradient sunColor;     // Colores del sol durante el día

    private float time;

    void Update()
    {
        if (sunLight == null) return;

        // Progreso del día (0 a 1)
        time += Time.deltaTime / dayLength;
        if (time > 1f) time = 0f;

        // Rotar el sol
        float rotation = time * 360f;
        transform.rotation = Quaternion.Euler(rotation, 0f, 0f);

        // Cambiar color del sol
        sunLight.color = sunColor.Evaluate(time);

        // Intensidad según ángulo
        float dot = Vector3.Dot(sunLight.transform.forward, Vector3.down);
        sunLight.intensity = Mathf.Clamp01(dot) * 1.5f;
    }
}
