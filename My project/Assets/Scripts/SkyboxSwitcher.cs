using UnityEngine;

public class SkyboxSwitcher : MonoBehaviour
{
    [Header("Skybox Materials")]
    public Material[] skyboxMaterials;
    private int index = 0;

    [Header("Modo de Cambio")]
    public bool changeWithKey = true;
    public KeyCode changeKey = KeyCode.K;

    public bool autoChange = false;
    public float interval = 20f;
    private float timer = 0f;

    void Start()
    {
        if (skyboxMaterials.Length > 0)
        {
            RenderSettings.skybox = skyboxMaterials[0];
            DynamicGI.UpdateEnvironment();
        }
    }

    void Update()
    {
        // --- CASO 1: CAMBIO CON TECLA ---
        if (changeWithKey)
        {
            if (Input.GetKeyDown(KeyCode.K))
            {
                NextSkybox();
            }
        }

        // --- CASO 2: CAMBIO AUTOMÁTICO ---
        if (autoChange)
        {
            timer += Time.deltaTime;
            if (timer >= interval)
            {
                timer = 0f;
                NextSkybox();
            }
        }
    }

    // --- CASO 3: CAMBIO POR BOTÓN UI ---
    public void NextSkybox()
    {
        if (skyboxMaterials.Length == 0)
            return;

        index++;
        if (index >= skyboxMaterials.Length)
            index = 0;

        RenderSettings.skybox = skyboxMaterials[index];

        // Refrescar iluminación
        DynamicGI.UpdateEnvironment();
    }
}
