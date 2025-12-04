using UnityEngine;
using UnityEngine.UI; // Necesario para UI

public class FossilActivator : MonoBehaviour
{
    public Animator fossilAnimator;
    public string animationTriggerName = "Activate";
    private bool playerInRange = false;

    public GameObject pressEText; // Referencia al UI

    void Start()
    {
        if (pressEText != null)
            pressEText.SetActive(false);
    }

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            fossilAnimator.SetTrigger(animationTriggerName);

            if (pressEText != null)
                pressEText.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;

            if (pressEText != null)
                pressEText.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;

            if (pressEText != null)
                pressEText.SetActive(false);
        }
    }
}
