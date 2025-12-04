using UnityEngine;
using TMPro;

public class UniversalInteraction : MonoBehaviour
{
    [Header("Interaction Settings")]
    public float activationDistance = 3f;
    public KeyCode interactionKey = KeyCode.E;

    [Header("Animator (opcional)")]
    public Animator targetAnimator;
    public string triggerName = "";

    [Header("Camera Switching (opcional)")]
    public Camera playerCamera;
    public Camera interactionCamera;

    [Header("UI")]
    public TextMeshProUGUI pressText;
    [TextArea]
    public string message = "Presiona E para interactuar";
    public string exitMessage = "Presiona E para salir";

    private Transform player;
    private bool isClose = false;
    private bool isViewing = false;

    void Start()
    {
        player = Camera.main.transform;

        if (pressText != null)
            pressText.gameObject.SetActive(false);

        if (interactionCamera != null)
            interactionCamera.gameObject.SetActive(false);
    }

    void Update()
    {
        if (player == null) return;

        float dist = Vector3.Distance(player.position, transform.position);

        // --------------------------
        //     NO ESTÁ EN VISTA
        // --------------------------
        if (!isViewing)
        {
            if (dist <= activationDistance)
            {
                if (!isClose)
                {
                    isClose = true;

                    if (pressText != null)
                    {
                        pressText.text = message;
                        pressText.gameObject.SetActive(true);
                    }
                }

                if (Input.GetKeyDown(interactionKey))
                {
                    ActivateInteraction();
                }
            }
            else
            {
                if (isClose)
                {
                    isClose = false;
                    if (pressText != null) pressText.gameObject.SetActive(false);
                }
            }
        }
        // --------------------------
        //     YA ESTÁ EN VISTA
        // --------------------------
        else
        {
            if (Input.GetKeyDown(interactionKey))
            {
                ExitInteraction();
            }
        }
    }

    void ActivateInteraction()
    {
        // 🔥 ANIMACIÓN (OPCIONAL)
        if (targetAnimator != null && triggerName != "")
            targetAnimator.SetTrigger(triggerName);

        // 🔥 CAMBIO DE CÁMARA
        if (interactionCamera != null)
        {
            isViewing = true;

            if (playerCamera != null)
                playerCamera.gameObject.SetActive(false);

            interactionCamera.gameObject.SetActive(true);

            if (pressText != null)
                pressText.text = exitMessage;
        }
        else
        {
            // Si no usa cámara simplemente ocultamos el texto
            if (pressText != null)
                pressText.gameObject.SetActive(false);
        }
    }

    void ExitInteraction()
    {
        if (interactionCamera != null)
            interactionCamera.gameObject.SetActive(false);

        if (playerCamera != null)
            playerCamera.gameObject.SetActive(true);

        isViewing = false;

        if (pressText != null)
            pressText.gameObject.SetActive(false);
    }
}
