using UnityEngine;
using TMPro;

public class CameraViewActivator : MonoBehaviour
{
    [Header("Interaction Settings")]
    public float activationDistance = 3f;
    public KeyCode key = KeyCode.E;

    [Header("References")]
    public Transform player;
    public Camera playerCamera;
    public Camera cuadroCamera;
    public TextMeshProUGUI pressText;

    private bool isPlayerClose = false;
    private bool isViewing = false;

    void Start()
    {
        if (pressText != null)
            pressText.gameObject.SetActive(false);

        if (cuadroCamera != null)
            cuadroCamera.gameObject.SetActive(false);

        if (player == null && Camera.main != null)
            player = Camera.main.transform;
    }

    void Update()
    {
        if (player == null) return;

        float dist = Vector3.Distance(player.position, transform.position);

        if (!isViewing) // Solo cuando NO estás viendo el cuadro
        {
            if (dist <= activationDistance)
            {
                if (!isPlayerClose)
                {
                    isPlayerClose = true;
                    if (pressText != null)
                        pressText.text = "Presiona E para ver";
                    if (pressText != null)
                        pressText.gameObject.SetActive(true);
                }

                if (Input.GetKeyDown(key))
                {
                    EnterViewMode();
                }
            }
            else
            {
                if (isPlayerClose)
                {
                    isPlayerClose = false;
                    if (pressText != null)
                        pressText.gameObject.SetActive(false);
                }
            }
        }
        else // Ya estás viendo el cuadro
        {
            if (Input.GetKeyDown(key))
            {
                ExitViewMode();
            }
        }
    }

    void EnterViewMode()
    {
        isViewing = true;

        // Cambiar cámaras
        playerCamera.gameObject.SetActive(false);
        cuadroCamera.gameObject.SetActive(true);

        if (pressText != null)
            pressText.text = "Presiona E para salir";
    }

    void ExitViewMode()
    {
        isViewing = false;

        cuadroCamera.gameObject.SetActive(false);
        playerCamera.gameObject.SetActive(true);

        if (pressText != null)
            pressText.gameObject.SetActive(false);
    }
}
